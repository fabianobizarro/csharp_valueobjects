# csharp_valueobjects
A set of value objects


# CPF

```cs
var cpf = new CPF("66672833102")

Console.WriteLine(cpf); // 66672833102
Console.WriteLine(cpf.ToString(true)); // 666.728.331-02
```

### TryParse

```cs
var valid = CPF.TryParse("adasdasdas", out var cpf);
Console.WriteLine(valid); // false
Console.WriteLine(cpf); // null


var valid = CPF.TryParse("66672833102", out var cpf);
Console.WriteLine(valid); // true
Console.WriteLine(cpf); // 66672833102
```