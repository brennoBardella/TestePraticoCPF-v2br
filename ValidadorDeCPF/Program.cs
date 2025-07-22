using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Digite seu CPF (Ex: xxx.xxx.xxx-xx): ");
        string cpf = Console.ReadLine();

        cpf = Regex.Replace(cpf, @"[^\d]", ""); // Remove caracteres

        cpf = cpf.PadLeft(11, '0'); // Preenche com zeros à esquerda

        Console.Clear();

        if (validarCPF(cpf))
        {
            Console.WriteLine("CPF válido.");
        }
        else
        {
            Console.WriteLine("CPF inválido."); // Validar o CPF
        }

        Console.WriteLine($"CPF informado: {cpf}");
        Console.WriteLine($"Seu CPF foi emitido em {estado(cpf)}.");
    }

    static bool validarCPF(string cpf) // Método para validar o CPF
    {
        if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
        {
            return false;
        }

        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += (cpf[i] - '0') * multiplicadores1[i];
        }

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if (digito1 != (cpf[9] - '0'))
        {
            return false;
        }

        soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += (cpf[i] - '0') * multiplicadores2[i];
        }

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return digito2 == (cpf[10] - '0');
    }

    static string estado(string cpf)
    {
        int digito = cpf[8] - '0';
        if (validarCPF(cpf)) // Verifica se o CPF é válido antes de determinar o estado
        { return digito switch
            {
                0 => "RS",
                1 => "DF, GO, MT, MS ou TO",
                2 => "PA, AM, AC, AP, RO ou RR",
                3 => "CE, MA ou PI",
                4 => "PE, RN, PB ou AL",
                5 => "BA ou SE",
                6 => "MG",
                7 => "RJ ou ES",
                8 => "SP",
                9 => "PR ou SC",
            };
        }else
        {
            return "CPF inválido, não é possível determinar o estado.";
        }
    }

}
