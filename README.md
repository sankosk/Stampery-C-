# Stampery
Notarize all your data using the blockchain. Generate immutable and valid globally proofs of existence, integrity and ownership of any piece of data.

## Arbitrary object stamping
```C#
string json = "{\"x\":\"true\"}";
Console.WriteLine(c.StampData(json));
```

## File Stamping
```C#
Dictionary<string, string> dict = new Dictionary<string, string>();
dict.Add("x", "true");
Console.WriteLine(c.StampFile(dict, @"C:\Users\Esteban\Documents\V0.zip"));
```

## Getting a stamp
```C#
Console.WriteLine(c.GetStamp("8d35d6a20140ac7dac9fdb9f51627899b20749ea87609f3a5d337dab5dff7c70"));
```

## PoC - Visualization
![screen](https://i.gyazo.com/963a28812757497357af2f85eca03a17.png)


