module day_6
open System.IO

let input = File.ReadAllText "day_6/input.txt" |> (fun s -> s.Split ",") |> Array.map int

// brute force approach
let problem1 =
    let days_count = 80
    let (result, _) = 
        days_count
            |> Array.zeroCreate
            |> Array.fold (fun (acc, fishes) _->
            //calculate new fishes
                let (nf, _) = ((fishes, 0), fishes) ||> Array.fold (fun (new_fishes, idx) fish ->
                    match fish with
                    //add a new fish, and reset this to 6
                    | 0 ->
                        Array.set new_fishes idx 6
                        let a =  Array.append new_fishes (Array.init 1 (fun _ -> 8))
                        (a, idx + 1)
                    | _ ->
                        Array.set new_fishes idx (fish - 1)
                        (new_fishes, idx + 1)
                )
                (Array.length nf, nf)
            ) (0, input)

    printfn "problem 1: %A" result
           

let init (cycle_count: int) =
        let arr: int64[] = Array.zeroCreate cycle_count
        input |> Array.iter (fun elem ->
            Array.set arr elem (arr.[elem] + int64 1)
        )
        arr

let problem2 =
    let days_count = 256
    let arr = init 9
    for _ = 0 to days_count - 1 do
        let new_fish_count = arr.[0]
        for j = 0 to 8 do
            match j with
            | 6 -> Array.set arr 6 (arr.[7] + new_fish_count)
            | 8 -> Array.set arr 8 (new_fish_count)
            | _ -> Array.set arr j (arr.[j + 1])

    printfn "problem 2: %A" (Array.sum arr)  