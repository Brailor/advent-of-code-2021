module one
open System.IO


let readLines (filePath:string) = seq {
    use sr = new StreamReader (filePath)
    while not sr.EndOfStream do
        yield sr.ReadLine () |> int
}

let problem1 depths = 
    (0, depths |> Seq.windowed 2) ||> Seq.fold (fun (inc_count) depth ->
        match depth with
        | [| int1; int2; |] -> if int1 < int2 then (inc_count + 1) else (inc_count)
        | _ -> (inc_count)
    )

let problem2 depths =
    let (count, _) = ((-1,0), depths |> Seq.windowed 3) ||> Seq.fold (fun (inc_count, prev_sum) depth -> 
        match depth with 
        | [|int1; int2; int3;|] ->
            let new_sum = int1 + int2 + int3
            match new_sum > prev_sum with
            | true -> (inc_count + 1, new_sum)
            | _ -> (inc_count, new_sum)
        | _ -> (inc_count, prev_sum)
    )
    count

