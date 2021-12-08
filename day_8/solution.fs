module day_8
open System.IO
open System

let input = 
    File.ReadAllLines "day_8/input.txt"
    |> Array.map (fun s -> (s.Split " | "))
    |> Array.map (fun line -> 
       let first_part = line |> Array.head |> (fun s -> s.Split " ")
       let second_part = line |> Array.tail |> Array.map (fun s -> s.Split " ") |> Array.concat

       (first_part, second_part)
    )

let problem1 =
    let res =
        input
        |> Array.map(fun (_, right) -> right)
        |> Array.concat
        |> Array.filter (fun segment -> 
            match String.length segment with
            | 2 | 3 |4| 7 -> true
            | _ -> false
        )
        |> Array.length
    printfn "problem 1: %A" res
   

let p example = 
    let mutable map: Map<int, string> = Map []
    for i = 0 to (Array.length example) - 1 do
        match String.length example.[i] with
        | 2 -> map <- Map.add 1 example.[i] map
        | 3 -> map <- Map.add 7 example.[i] map
        | 4 -> map <- Map.add 4 example.[i] map
        | 7 -> map <- Map.add 8 example.[i] map
        |_ -> ()

    for i = 0 to (Array.length example) - 1 do
        match String.length example.[i] with
        | 5 -> 
            let one = (Map.find 1 map) |> Set.ofSeq
            let four = (Map.find 4 map) |> Set.ofSeq
            let num = example.[i] |> Set.ofSeq
            if (Set.isProperSubset one num) then
                map <- Map.add 3 example.[i] map
            else if (Set.isProperSubset (Set.difference four one) num) then
                map <- Map.add 5 example.[i] map
            else
                map <- Map.add 2 example.[i] map
        | 6 ->
            let one = (Map.find 1 map) |> Set.ofSeq
            let four = (Map.find 4 map) |> Set.ofSeq
            let num = example.[i] |> Set.ofSeq
            if (Set.isProperSubset four num) then
                map <- Map.add 9 example.[i] map
            else if (Set.isProperSubset one num) then
                map <- Map.add 0 example.[i] map
            else 
                map <- Map.add 6 example.[i] map
        | _ -> ()

    map


let sortedString (str : string) = str |> Seq.sort |> String.Concat

let problem2 =
    let x =  (0, input) ||> Array.fold(fun v (left, right) -> 
        let r = p left
        let r2 = right |> Array.map (fun str ->
            let m = r |> Map.toArray
            let (num, _) = m |> Array.find (fun (_, string) -> 
                (sortedString str) = (sortedString string)
            )
            num
        )
        v +  (int($"{r2.[0]}{r2.[1]}{r2.[2]}{r2.[3]}"))
    )
    printfn "problem 2: %A" x
