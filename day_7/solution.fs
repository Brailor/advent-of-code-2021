module day_7
open System.IO
open System
open FSharp.Stats

let input = File.ReadAllText "day_7/input.txt" |> (fun s -> s.Split  ",") |> Array.toSeq |> Seq.map float


let problem1 = 
    let median = input |> Seq.median

    let res = 
        input 
        |> Seq.map (fun elem -> abs (elem - median))
        |> Seq.sum
    printfn "problem 1: %A" (int res)


let problem2 = 
    let mean = input |> Seq.mean
    let min_mean = floor mean
    let max_mean = ceil mean

    let (min_sum, _) =
        input
        |> Seq.map (fun elem -> 
            let dis = abs (elem - max_mean)
            let max_sum = (Math.Pow(dis, 2.0) + dis)/ 2.0

            let dis2 = abs (elem - min_mean)
            let min_sum = (Math.Pow(dis2, 2.0) + dis2)/ 2.0
                    
            (min_sum, max_sum)
        )
        |> Seq.fold (fun (min_acc, max_acc) (min_sum, max_sum) -> (min_acc + min_sum, max_acc + max_sum)) (0.0, 0.0)
        

    printfn "problem 2: %A" (int min_sum)

