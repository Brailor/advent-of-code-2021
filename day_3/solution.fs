module day_3
open System.IO

let input = File.ReadAllLines "day_3/input.txt" |> Array.toList

let bitCount input = String.length (List.head input)
let inline charToInt c = int c - int '0'

let getMostCommon (input1: list<string>) (pos:int) =
    (0, input1) ||> List.fold (fun counter binary -> 
        counter + (charToInt binary.[pos]))


let problem1 =
    let input_lenght = float(List.length input) / 2.0
    let a = List.init (bitCount input) (fun _ -> 0) |> List.mapi (fun i _ ->
        let mostCommon = (input, i) ||> getMostCommon |> (fun most_common -> if (float most_common) >= input_lenght then '1' else '0')

        match mostCommon with
        | '0' -> ('0', '1')
        | '1' -> ('1', '0')
        | _ -> failwith "error parsing")
    
    let (gamma, epsilon) = (("", ""), a) ||> List.fold (fun (acc_a, acc_b) (a, b) -> ($"{acc_a}{a}", $"{acc_b}{b}"))
   
    
    int $"0b{gamma}" * int $"0b{epsilon}"

let problem2 =
    let rec ox list pos =
        match list with
        | [] -> []
        | [a] -> [a]
        | _ ->
            let input_lenght = (float (List.length list) / 2.0)
            let mostCommon = (list, pos) ||> getMostCommon |> (fun most_common -> if (float most_common) >= input_lenght then '1' else '0')
            let new_input = list |> List.filter (fun binary ->
                let desired_bit = (Seq.toList binary).[pos]
        
                desired_bit = mostCommon)
            ox new_input (pos + 1)

    let rec co list pos =
        match list with
        | [] -> []
        | [a] -> [a]
        | _ ->
            let input_lenght = (float (List.length list) / 2.0)
            let mostCommon =  (list, pos) ||> getMostCommon |> (fun most_common -> if (float most_common) >= input_lenght then '0' else '1')
            let new_input = list |> List.filter (fun binary ->
                let desired_bit = (Seq.toList binary).[pos]
        
                desired_bit = mostCommon)
            co new_input (pos + 1)


    let r1 = ox input 0 
    printfn "ass %A" r1
    let r2 = co input 0
    printfn "ass2 %A" r2
    printfn "result: %A" ((int $"0b{r1.[0]}") * (int $"0b{r2.[0]}"))

