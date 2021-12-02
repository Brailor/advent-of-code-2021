module one
open System
open System.IO


let readLines (filePath:string) = seq {
    use sr = new StreamReader (filePath)
    while not sr.EndOfStream do
        yield sr.ReadLine () |> int
}

let depths = readLines "one/input.txt"
let problem1 = 
    // let depths = seq [1; 2; 3; 4; 5;]
    let mutable counter = 0
    depths |> Seq.windowed 2 |> Seq.iter (fun (elems) ->
        if elems.[0] < elems.[1] then
            counter <- counter + 1
    )

    printf "%A" counter    

let problem2 =
    let mutable counter = 0

    let res = depths |> Seq.windowed 3 |> Seq.map (fun (elems) ->
        Array.sum elems) |> Seq.windowed 2 |> Seq.iter (fun (elems) ->
        if elems.[0] < elems.[1] then
            counter <- counter + 1
    )  
    printf "%A" counter

