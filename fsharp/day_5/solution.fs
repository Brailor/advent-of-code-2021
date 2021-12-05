module day_5
open System.IO

let input = File.ReadAllLines "day_5/input.txt" 

type Vec = int * int
type VecList = array<(Vec * Vec)>
type Matrix = array<array<int>>

let parsed = 
    input |> Array.map (fun part ->
        // part: "x1,y1 -> x2,y2"
        // want: ((x1, y2), (x2, y2))
        let s1 = part.Split " -> "

        let make_pairs (s:string) =
            let splitted = s.Split ","
            match splitted with
            | [|a; b;|] -> (int a, int b)
            | _ -> failwith "error during parsing"

        match s1 with
        |[|a; b;|] -> (make_pairs a, make_pairs b)
        | _ -> failwith "error during parsing"
    )
    
let filter_diagonals (list: VecList) = 
    list |> Array.filter (fun (vec1, vec2) ->
       let (x1, y1) = vec1
       let (x2, y2) = vec2

       x1 = x2 || y1 = y2
    )

let get_max (list: VecList) = 
    list |> Array.fold (fun (max_x, max_y) (vec1, vec2) -> 
        let (x1, y1) = vec1
        let (x2, y2) = vec2

        (max max_x (max x1 x2), max max_y (max y1 y2))

    ) (0, 0)

let make_matrix (list: VecList) =
    let (max_x, max_y) = get_max list
    
    Array.init (max_y + 1)(fun _ ->
        Array.init (max_x + 1) (fun _ ->
            0
        )
    )

let mark_matrix (matrix: Matrix) (col_row: int * int) =
    let (col, row) = col_row

    matrix |> Array.mapi (fun col_idx rw ->
        if col_idx = col then
            rw |> Array.mapi (fun row_idx elem -> 
                if row_idx = row then
                    elem + 1
                else
                    elem
            )
        else 
            rw
    )
    

let calculate_crossings (matrix: Matrix) = 
    (0, matrix) ||> Array.fold (fun count row ->
        (count, row) ||> Array.fold (fun c2 elem ->
            if elem > 1 then
                c2 + 1
            else
                c2
        )
    )


let calculate_result (matrix: Matrix) (filtered: VecList) = 
    let result = (matrix, filtered) ||> Array.fold (fun prev_matrix (vec1, vec2) ->
        let (x1, y1) = vec1
        let (x2, y2) = vec2
        let mutable new_matrix = prev_matrix

        let mutable next_x = x1
        let mutable next_y = y1
        for i = 0 to max (abs (x1 - x2)) (abs (y1 - y2)) do
            if x1 - x2 < 0 then
                next_x <- x1 + i
            else if x1 - x2 > 0 then
                next_x <- x1 - i 
            if y1 - y2 < 0 then
                next_y <- y1 + i
            else if y1 - y2 > 0 then
                next_y <- y1 - i

            new_matrix <- (mark_matrix new_matrix (next_x, next_y))

        new_matrix)

    result

let problem1 =
    let filtered = parsed |> filter_diagonals
    let matrix = filtered |> make_matrix
    let result = calculate_result matrix filtered
    printfn "result for problem 1: %A" (calculate_crossings result)

let problem2 =
    let matrix = parsed |> make_matrix
    let result = calculate_result matrix parsed
    printfn "result for problem 2: %A" (calculate_crossings result)


    