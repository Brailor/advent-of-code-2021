module day_13

let parse file =
    file
    |> System.IO.File.ReadAllLines
    |> Array.filter (fun str -> str.Length <> 0)
    |> Array.fold (fun (input, commands) line ->
        let parts = line.Split ","
        match parts with
        | [|col; row;|] -> (( [|(int col, int row)|] |> Array.append input), commands)
        | _ ->
            let command = line.Split "="
            match command with
            | [|dir; cord;|] -> (input, ( [|(dir.[dir.Length - 1], int cord)|] |> Array.append commands))
            | _ -> failwith "bad input!"
    ) (Array.empty, Array.empty)

let get_cols_rows input =
    input
    |> Array.fold (fun (x, y) (curr_x, curr_y) ->
        ((if curr_x > x then curr_x else x), (if curr_y > y then curr_y else y))
    ) (0, 0)

let print_map (map: Map<int * int, char>) (cols: int, rows: int) =
        for row = 0 to rows do
            for col = 0 to cols do
                printf "%A" (map |> Map.find (col, row))
            printf "\n"

let rotate_180 (map: Map<int * int, char>) (rows: int) =
    let mutable arr = map |> Map.toArray |> Array.chunkBySize rows
    let mutable down_counter = rows - 1
    printfn "arr before rotate: %A" map
    for counter = 0 to (rows - 1) / 2 do
        let first_row = arr.[counter]
        let last_row = arr.[down_counter]
        arr.[counter] <- last_row
        arr.[down_counter] <- first_row
        down_counter <- down_counter - 1
    printfn "arr after rotate: %A" arr
    let mutable new_map = Map.empty
    for row = 0 to rows - 1 do
        for col = 0 to (arr.[row] |> Array.length) - 1 do
            let (_, v) = arr.[row].[col]
            new_map <- new_map |> Map.add (col, row) v
    print_map new_map (3, 3)
    0


let problem =
    let (input, _) = parse "day_13/example.txt"
    let (cols, rows) = get_cols_rows input

    printfn "parsed: %A" input
    printfn "dimensions: %A" (cols, rows)
    let mutable map = Map.empty
    for row = 0 to rows do
        for col = 0 to cols do
            let key = (col, row)
            map <- map |> Map.add key '.'
    
    for cord in input do
        map <- map |> Map.change cord (fun v ->
            match v with
            | None -> None
            | Some _ -> Some ('#')
        )
    printfn "map: %A" map
    print_map map (cols, rows)
    rotate_180 map 4 |> ignore
    
    0