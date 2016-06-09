namespace SylCode

open System.Collections
open System

module SylCode =

    // By Gennady Loskutov from http://fssnip.net/gf
    let fromHex (s:string) = 
      s
      |> Seq.windowed 2
      |> Seq.mapi (fun i j -> (i,j))
      |> Seq.filter (fun (i,j) -> i % 2=0)
      |> Seq.map (fun (_,j) -> Byte.Parse(new System.String(j),System.Globalization.NumberStyles.AllowHexSpecifier))
      |> Array.ofSeq


    let letters = ['a'..'z']
    let vowels = ['a'; 'e'; 'i'; 'o'; 'u'; 'y']

    let double_ctos c = sprintf "%c%c" c c
    let ctos c = sprintf "%c" c

    let doublevowels = List.map double_ctos vowels
    let vowelstrings = "ai" :: List.map ctos vowels // one extra vowel combination needed to exceed 256 different syllables

    let all_vowels = List.append vowelstrings doublevowels

    let consonants =
        let consonant_set = (Set letters) - (Set vowels)
        Set.toList consonant_set

    let syllables = [ for x in consonants do
                        for y in all_vowels do
                            yield sprintf "%c%s" x y 
                    ]

    let byte_to_syl (input:byte) : string = syllables.Item (int input)

    let encode (value:string) =
        let ByteData = fromHex value
        System.String.Concat(Array.map byte_to_syl ByteData)
