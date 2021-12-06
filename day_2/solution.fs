module day_2
open System.IO


let readLines (filePath:string) = seq {
    use sr = new StreamReader (filePath)
    while not sr.EndOfStream do
        let line = sr.ReadLine ()
        let res = 
            match line.Split " " with
            | [| var1; var2; |] -> (var1, int var2)
            | _ -> failwith "something bad happend"

        yield res
}
let problem1 input  =
    let (x, y) = ((0,0), input) ||> Seq.fold (fun (x, y) action ->
        match action with
        | ("forward", num) -> (x + num, y)
        | ("down", num) -> (x, y + num)
        | ("up", num) -> (x, y - num)
        | _ -> failwith "error"
    )
    x * y

let problem2 input =
    let (x, y, _) = ((0, 0, 0), input) ||> Seq.fold (fun (x, y, aim) action ->
        match action with
        | ("forward", num) -> (x + num, y + aim * num, aim)
        | ("down", num) -> (x, y, aim + num)
        | ("up", num) -> (x, y, aim - num)
        | _ -> failwith "error"
    )
    x * y
