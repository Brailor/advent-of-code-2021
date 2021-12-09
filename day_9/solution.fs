module day_9
open System.IO


let example = File.ReadAllLines "day_9/input.txt" 

let width = (example |> Array.head |> String.length) - 1
let height = (example |> Array.length) - 1
    
let left_up_corner (x, y) = [|(x + 1, y); (x, y + 1)|]
let left_bottom_corner (x, y) = [|(x + 1, y); (x, y - 1)|]
let right_up_corner (x, y) = [|(x - 1, y); (x, y + 1)|]
let right_bottom_corner (x, y) = [|(x - 1, y); (x, y - 1)|]
let first_row_neighbors (x, y) = [|(x - 1, y); (x + 1, y); (x, y + 1)|]
let first_col_neiggbors (x, y) = [|(x, y + 1); (x, y - 1); (x + 1, y)|]
let last_row_neighbors (x, y) = [|(x, y - 1); (x - 1, y); (x + 1, y)|]
let last_col_neighbots (x, y) = [|(x, y + 1); (x, y - 1); (x - 1, y)|]
let neighbors (x, y) = [|(x + 1, y); (x - 1, y); (x, y + 1); (x, y - 1)|]

let get_neighbors (x, y) = 
    match (x, y) with
    | (0, 0) ->  left_up_corner (x, y)
    | (0, y) when y = height -> left_bottom_corner (x, y)
    | (x, 0) when x = width -> right_up_corner (x, y)
    | (x, y) when x = width && y = height -> right_bottom_corner (x, y)
    | (_, y) when y = height -> last_row_neighbors (x, y)
    | (x, _) when x = width -> last_col_neighbots (x, y)
    | (0, _) -> first_col_neiggbors (x, y)
    | (_, 0) -> first_row_neighbors (x, y)
    | (_, _) -> neighbors (x, y)

let get_el (matrix: array<string>) (x: int, y: int) =
    Array.get matrix y |> (fun s -> s.[x])
let inline charToInt c = int c - int '0'
let get_element = get_el example

let is_smallest_item (item: char) (neighbors: array<char>) = 
    let item_num = charToInt item
    let neighbors_num = neighbors |> Array.map charToInt
    item_num < (Array.min neighbors_num)


let rec basin (next_point: int * int) (checked_points: Set<int * int>) =
    let neighbors_cords = get_neighbors next_point
    let neighbors = neighbors_cords |> Array.map get_element
    let zipped = Array.zip neighbors neighbors_cords
    let neighbors_filtered = zipped |> Array.map (fun (num, a) -> (charToInt num), a) |> Array.filter (fun (num, _) -> num <> 9)
    let lowest_point_num = get_element next_point |> charToInt

    let res = neighbors_filtered |> Array.filter (fun (num, _) -> (num > lowest_point_num)) |> Array.map (fun (_, point) -> point)

    match res with
    | [||] -> checked_points
    | _ ->
        let new_res = (checked_points, res) ||> Array.fold (fun cp r -> cp |> Set.add r)
        (new_res, res) ||> Array.fold (fun cp x -> cp |> Set.union (basin x cp ))



let problem1 =
    let mutable lowest_points: array<int> = [||]
    let mutable lowest_points_coords: array<int * int> = [||]
    example |> Array.iteri (fun y_idx row ->
        row |> String.iteri (fun x_idx item ->
            let ns = get_neighbors (x_idx, y_idx) |> Array.map get_element
            if is_smallest_item item ns then
                    lowest_points <- Array.append lowest_points [|charToInt item|]
                    lowest_points_coords <- Array.append lowest_points_coords [|(x_idx, y_idx)|]
        )
    )

    let r =
        lowest_points_coords 
        |> Array.map (fun x -> basin x (Set.ofArray [|x|]) |> Set.toArray |> Array.length)
        |> Array.sort
        |> Array.rev
        |> Array.take 3
        |> Array.reduce (fun a b -> a * b)
    printfn "basin r: %A" r
    printfn "res: %A" (lowest_points |> Array.map ((+) 1) |> Array.sum)