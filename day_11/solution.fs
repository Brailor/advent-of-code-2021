module day_11

open System.IO
open Microsoft.FSharp.Collections

let inline charToInt (c: char) = int c - int '0'

let input =
    File.ReadAllLines "day_11/input.txt"
    |> Array.map (fun s -> s |> Seq.map charToInt)
    |> array2D

let left_up_corner (x, y) =
    [| (x + 1, y)
       (x, y + 1)
       (x + 1, y + 1) |]

let left_bottom_corner (x, y) =
    [| (x + 1, y)
       (x, y - 1)
       (x + 1, y - 1) |]

let right_up_corner (x, y) =
    [| (x - 1, y)
       (x, y + 1)
       (x - 1, y + 1) |]

let right_bottom_corner (x, y) =
    [| (x - 1, y)
       (x, y - 1)
       (x - 1, y - 1) |]

let first_row_neighbors (x, y) =
    [| (x - 1, y)
       (x + 1, y)
       (x, y + 1)
       (x - 1, y + 1)
       (x + 1, y + 1) |]

let first_col_neighbors (x, y) =
    [| (x, y + 1)
       (x, y - 1)
       (x + 1, y)
       (x + 1, y - 1)
       (x + 1, y + 1) |]

let last_row_neighbors (x, y) =
    [| (x, y - 1)
       (x - 1, y)
       (x + 1, y)
       (x - 1, y - 1)
       (x + 1, y - 1) |]

let last_col_neighbors (x, y) =
    [| (x, y + 1)
       (x, y - 1)
       (x - 1, y)
       (x - 1, y - 1)
       (x - 1, y + 1) |]

let neighbors (x, y) =
    [| (x + 1, y)
       (x - 1, y)
       (x, y + 1)
       (x, y - 1)
       (x - 1, y - 1)
       (x + 1, y - 1)
       (x - 1, y + 1)
       (x + 1, y + 1) |]

let height = (input |> Array2D.length1) - 1
let width = (input |> Array2D.length2) - 1

let get_neighbors (x: int, y: int) =
    match (x, y) with
    | (0, 0) -> left_up_corner (x, y)
    | (0, y) when y = height -> left_bottom_corner (x, y)
    | (x, 0) when x = width -> right_up_corner (x, y)
    | (x, y) when x = width && y = height -> right_bottom_corner (x, y)
    | (_, y) when y = height -> last_row_neighbors (x, y)
    | (x, _) when x = width -> last_col_neighbors (x, y)
    | (0, _) -> first_col_neighbors (x, y)
    | (_, 0) -> first_row_neighbors (x, y)
    | (_, _) -> neighbors (x, y)

let get_element (x: int, y: int) (matrix: int [,]) = (y, x) ||> Array2D.get matrix

let problem1 =
    let rec flash_neighbor (matrix: int [,]) (nx: int, ny: int) =
        let neighbors = get_neighbors (nx, ny)

        neighbors
        |> Array.iter (fun (nx, ny) ->
            let neighbor_val = (get_element (nx, ny) matrix) + 1
            Array2D.set matrix ny nx neighbor_val)

    let do_flash (matrix: int [,]) =
        let mutable flash_count = 0
        let cloned = Array2D.copy matrix
        let mutable already_flashed = Set.empty<int * int>

        let rec flash_neighbor cloned =
            cloned
            |> Array2D.iteri (fun y x v ->
                if
                    v > 9
                    && not (already_flashed |> Set.contains (x, y))
                then
                    already_flashed <- already_flashed |> Set.add (x, y)
                    // add +1 to all neighbors, then set the new neighbors values to the matrix
                    let neighbors = get_neighbors (x, y)

                    neighbors
                    |> Array.iter (fun (nx, ny) ->
                        let neighbor_val = (get_element (nx, ny) cloned) + 1
                        Array2D.set cloned ny nx neighbor_val

                        if
                            (neighbor_val > 9)
                            && not (already_flashed |> Set.contains (nx, ny))
                        then
                            flash_neighbor cloned))

        flash_neighbor cloned

        let r =
            cloned
            |> Array2D.map (fun v ->
                match v with
                | a when a > 9 ->
                    flash_count <- flash_count + 1
                    0
                | _ -> v)

        (r, flash_count)

    let flat2Darray array2D =
        seq {
            for x in [ 0 .. (Array2D.length1 array2D) - 1 ] do
                for y in [ 0 .. (Array2D.length2 array2D) - 1 ] do
                    yield array2D.[x, y]
        }

    let rec stepper (matrix: int [,]) (step: int) (flash_count: int) =
        match step with
        | 229 -> (matrix, flash_count)
        | _ ->
            //1. First, the energy level of each octopus increases by 1.
            let new_matrix = matrix |> Array2D.map ((+) 1)

            //2. Then, any octopus with an energy level greater than 9 flashes
            // this increases the energy level of all adjacent octopuses by 1
            let (m, count) = do_flash new_matrix

            let flattend =
                flat2Darray m |> Seq.forall (fun elem -> elem = 0)

            if flattend then
                printfn "Every val is zero at step: %A" (step + 1)
                printfn "matrix:"
                printfn "%A" m

            stepper m (step + 1) (count + flash_count)

    let (_, res) = stepper input 0 0
    printfn "res: %A" res
    res
