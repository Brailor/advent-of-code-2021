// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp


open one
open two
open System

[<EntryPoint>]
let main argv =
    let input1 = one.readLines "one/input.txt"
    let d11 = one.problem1 input1
    let d12 = one.problem2 input1
    let input2 = two.readLines "two/input.txt"
    let d21 = two.problem1 input2
    let d22 = two.problem2 input2

    printfn "day - 1: \n problem - 1: %A \n problem - 2: %A" d11 d12

    printfn "day - 2: \n problem - 1: %A \n problem - 2: %A" d21 d22

    0 // return an integer exit code