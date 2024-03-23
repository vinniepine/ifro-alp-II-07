/*  Instituto Federal de Educação, Ciência e Tecnologia de Rondônia - IFRO
 *  C.S.T. em Análise e Desenvolvimento de Sistemas
 *  Disciplina de Algoritmo e Lógica de Programação II
 *  Professor Clayton Ferraz de Andrade
 *  
 *  Acadêmico Vinicius de Oliveira Pinheiro
 *  Atividade n. 03 de ALP II 
 *  - Aula do dia 18-09-2023
 */


using System.Numerics;
using System.Security.Cryptography.X509Certificates;
// Exercício 01
void Exercise01()
{
    static int[] SetInOrder(int a, int b, int c)
    {
        int x;
        if (a > b)
        {
            x = a;
            a = b;
            b = x;
        }
        if (b > c)
        {
            x = b;
            b = c;
            c = x;
        }
        if (a > b)
        {
            x = a;
            a = b;
            b = x;
        }
        int[] number = { a, b, c };
        return number;
    }
    Console.Clear();
    Console.Write("\n\n\n\n");
    Console.WriteLine("    ==== ORDENADOR DE NÚMEROS ====\n");
    Console.Write("        Este algoritmo coloca três algarismos digitados em ordem.\n");
    Console.Write("        Digite o primeiro número: "); int first = int.Parse(Console.ReadLine()!);
    Console.Write("        Digite o segundo número: "); int second = int.Parse(Console.ReadLine()!);
    Console.Write("        Digite o terceiro número: "); int third = int.Parse(Console.ReadLine()!);
    int[] inOrder = SetInOrder(first, second, third);

    Console.Write($"        Os números ordenados são: {inOrder[0]}, {inOrder[1]}, {inOrder[2]}.");

    Console.ReadLine();
}

// Exercício 02
void Exercise02()
{
    int[][] matrixA = null;
    int option;
    bool on = true;
    do
    {
        Console.Clear();
        Console.Write("\n\n\n\n");
        Console.WriteLine("    ==== ANÁLISE DE MATRIZ ====\n");
        Console.WriteLine("        Menu:\n");
        Console.WriteLine("        1. Preencher a matriz [A];");
        Console.WriteLine("        2. Ordenar elementos da matriz [A] em ordem crescente;");
        Console.WriteLine("        3. Gerar uma matriz somente com os números pares da matriz [A];");
        Console.WriteLine("        4. Gerar uma matriz somente com os múltiplos de 5 da matriz [A];");
        Console.WriteLine("        5. Sair.\n");
        Console.Write("        ");


        option = int.Parse(Console.ReadLine()!);

        switch (option)
        {
            case 1 or 01:
                matrixA = FillingTheMatrix();
                break;
            case 2 or 02:
                if (matrixA != null)
                {
                    SetMatrixInOrder(matrixA);
                }
                else
                {
                    Console.Write("        A matriz ainda não foi preenchida.");
                }
                break;
            case 3 or 03:
                if (matrixA != null)
                {
                    int[] evenNumbers = EvenNumbers(matrixA);
                    PrintArray(evenNumbers);
                }
                else
                {
                    Console.WriteLine("        Você ainda não preencheu a matriz.");
                }
                break;
            case 4 or 04:
                if (matrixA != null)
                {
                    int[] multiplesOf5 = MultiplesOf5(matrixA);
                    PrintArray(multiplesOf5);
                }
                else
                {
                    Console.Write("        Você ainda não preencheu a matriz.");
                }
                break;
            case 5 or 05:
                on = false;
                break;
            default:
                Console.Write("        Não entendi. Poderia digitar de novo?");
                break;
        }
    } while (on);
    static int[][] FillingTheMatrix()
    {
        Console.Clear();
        Console.WriteLine("\n\n\n\n");
        Console.Write("        Digite o número de linhas da matriz: ");
        int rows = int.Parse(Console.ReadLine()!);
        int[][] matrix = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            Console.Write($"\n        Digite o número de colunas da linha {i + 1} desta matriz: ");
            int columns = int.Parse(Console.ReadLine()!);
            matrix[i] = new int[columns];
            Console.Write($"        Por favor, preencha a linha {i + 1}: \n\n");
            for (int j = 0; j < columns; j++)
            {
                Console.Write($"        >> Elemento {j + 1} da coluna {i + 1}: ");
                matrix[i][j] = int.Parse(Console.ReadLine()!);
            }
        }
        return matrix;
    }
    static void SetMatrixInOrder(int[][] matrix)
    /* Tentei por diversas vezes fazer um método do tipo inteiro (um que retornasse um valor inteiro) que,
     * depois de reordernar os elementos, retornasse a mesma variável de matriz na saída, mas reordenada.
     * Enretanto, quando há uma modificação do parâmetro de entrada, percebi que devo usar métodos 'void',
     * e não uma função que ofereça um retorno. Para retornar um resultado, eu precisaria criar uma nova
     * matriz que coletasse os dados da ordenados da matriz de entrada.
     * 
     * Obs.: se eu quisesse apresentar o método de forma reordenada na hora que fosse apresentar os números
     * pares e múltiplos de 5, o ideal seria fazer uma função com retorno em int, já que, sendo void, esta
     * função não podera retorna valor, e portanto não pode ser passada como argumento dentro da função que
     * seleciona apenas os números pares, ou apenas os múltiplos de 5;
     */
    {
        /* já que para cada linha de minha matriz, ela possui colunas de tamanhos diferentes,
         * eu não posso simplesmente criar uma nova matriz achatada a partir da multiplicação
         * do número de linhas pelo número de colunas; minha matrix é uma "jagged matrix"; é
         * irregular;
         */

        int totalElements = 0; // dará o tamanho de nosso vetor unidimensional
        /* Esta próxima parte calcula o número total de elementos dentro do vetor irregular, e
         * fá-lo ao iterar por cada linha (int[] row in matrix) do vetor irregular e adicionando
         * o comprimento de cada linha à variável "totalElements". Esta parte é necessária para
         * determinar o tamanho do vetor unidimensional que será responsável por guardar todas os
         * os elementos da matriz irregular quando for achatada.
         */

        foreach (int[] row in matrix)
        {
            totalElements += row.Length;
        }
        int[] flattenedMatrix = new int[totalElements];
        /* O vetor 'flattenedMatrix' tem um tamanho igual a 'totalElements', calculado no passo 
         * anterior. Esse vetor armazenará temporariamente todos os elementos do vetor irregular 
         * (jagged array);
         */
        int index = 0;
        foreach (int[] row in matrix)
        {
            /* Esta parte do código itera em cada linha do vetor irregular 'matrix' e, para cada 
             * elemento da linha, atribui o elemento à posição correspondente em 'flattenedMatrix'. 
             * A variável 'index' vai registrar a posição atual no vetor achatado.
             */
            foreach (int element in row)
            {
                flattenedMatrix[index] = element;
                index++;
            }
        }
        /* Depois de achatar a matrix, ou vetor irregular, e armazenar seus elementos em 
         * ‘flattenedMatrix’, posso utilizar o método 'Array.Sort' para que os elementos 
         * sejam reordenados em ordem crescente.
         */

        Array.Sort(flattenedMatrix);
        index = 0;
        for (int i = 0; i < matrix.Length; i++)
        {
            /* Por fim, itera-se pelo vetor irregular novamente (matrix). Para cada linha, iteramos
             * por todos os elementos naquelas linhas substituímo-los pelos valores que estavam em 'flattenedMatrix', 
             * atualizando o vetor irregular original com os elementos reordenados.
             */
            for (int j = 0; j < matrix[i].Length; j++)
            {
                matrix[i][j] = flattenedMatrix[index];
                index++;
            }
        }
        // A 'matrix' conterá o mesmo número de elementos que antes, mas serão reordenados de forma crescente.
        PrintMatrix(matrix); // chama o método de impressão, passando como argumento a matriz reorganizada
    }
    // Preciso, agora, criar uma nova matriz para armazenar os valores ordenados

    static int[] EvenNumbers(int[][] matrix)
    { 
    Console.Clear();
    int counter = 0;
    for (int i = 0; i < matrix.Length; i++)
    {
        for (int j = 0; j < matrix[i].Length; j++)
        {
            if (matrix[i][j] % 2 == 0)
            {
                counter++;
            }
        }
    }
    int[] matrixElements = new int[counter];
    int index = 0;
    for (int i = 0; i < matrix.Length; i++)
    {
        for (int j = 0; j < matrix[i].Length; j++)
        {
            if ((matrix[i][j] % 2 == 0))
            {
                matrixElements[index] = matrix[i][j];
                index++;
            }
        }
    }
    Console.Write("\n\n\n\n");
    Console.WriteLine("        Números pares da matriz: ");
    Console.WriteLine(string.Join(", ", matrixElements));
    return matrixElements;
    }

    static int[] MultiplesOf5(int[][] matrix)
    {
        Console.Clear();
        int counter = 0;
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j] % 5 == 0)
                {
                    counter++;
                }
            }
        }
        int[] matrixElements = new int[counter];
        int index = 0;
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if ((matrix[i][j] % 5 == 0))
                {
                    matrixElements[index] = matrix[i][j];
                    index++;
                }
            }
        }
        Console.Write("\n\n\n\n\n\n\n\n");
        Console.WriteLine("        Múltiplos de 5 da matriz: ");
        Console.WriteLine(string.Join(", ", matrixElements));
        return matrixElements;
    }

    static void PrintMatrix(int[][] matrix)
    {
        Console.Clear();
        Console.Write("\n\n\n\n");
        Console.Write("    Matriz:\n");
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                Console.WriteLine($"    {matrix[i][j]}");
            }
        }
        Console.ReadLine();
    }
    static void PrintArray(int[] array)
    {
        Console.Clear();
        Console.Write("\n\n\n\n");
        Console.Write("    Matriz: ");
        Console.Write(string.Join(", ", array.Where(x => x!=0)));
        Console.ReadLine();
    }

}

// Exercício 03
void Exercise03()   
{
    Console.Clear();
    Console.WriteLine("\n\n\n\n");
    Console.WriteLine("    ==== CONVERSOR DE MOEDAS ====\n");
    Console.Write("    Esse algoritmo converte valores em Real Brasileiro para outras moedas.\n");
    Console.ReadKey();
    Console.Write("    Valor em BRL (R$) que deseja converter: ");
    double brlValue = double.Parse(Console.ReadLine()!);
    bool running = true;

    while (running)
    {
        Console.Clear();
        Console.WriteLine("\n\n\n\n");
        Console.Write("    Para qual moeda deseja converter?\n\n    1) Dólar Americano (USD);\n    2) Franco Suíço (CHF);\n    3) Libra Esterlina (GBP);\n    4) Iene Japonês (JPY);\n    5) Euro (EUR) \n    6) Sair.\n\n    Digite apenas o número correspondente: ");
        int option = int.Parse(Console.ReadLine()!);
        switch (option)
        {
            case 1 or 01: { Console.Write($"    {brlValue:C2} = US$ {Math.Round(Currencies(brlValue, "USD"), 2)}. "); Console.ReadKey();  break; }
            case 2 or 02: { Console.Write($"    {brlValue:C2} = SFr {Math.Round(Currencies(brlValue, "CHF"), 2)} (francos suíços). "); Console.ReadKey(); break; }
            case 3 or 03: { Console.Write($"    {brlValue:C2} = £ {Math.Round(Currencies(brlValue, "GBP"), 2)}. "); Console.ReadKey(); break; }
            case 4 or 04: { Console.Write($"    {brlValue:C2} = ¥ {Math.Round(Currencies(brlValue, "JPY"), 2)}. "); Console.ReadKey(); break; }
            case 5 or 05: { Console.Write($"    {brlValue:C2} = {Math.Round(Currencies(brlValue, "EUR"), 2)} €. "); Console.ReadKey(); break; }
            case 6 or 06: running = false; break;
            default: { Console.Write("    Moeda inválida"); Console.ReadKey(); break; }
 
        }
    }
    double Currencies(double brl, string currency)
    {
        // Dados do câmbio colhidos às 15h42m do dia 18/09/2023;
        // Meio de busca: USD to BRL full; CHF to USD full; GBP to USD full; JPY to USD full;
        double usd = brl * 0.205506;
        if (currency == "CHF") { double chf = usd * 1.11431; return chf; }
        else if (currency == "GBP") { double gbp = usd * 1.23857; return gbp; }
        else if (currency == "JPY") { double jpy = usd * 147.53; return jpy; }
        else if (currency == "EUR") { double eur = usd * 1.08286; return eur; }
        else { return usd; }
    }
}

// Execício 04
void Exercise04()
{
    bool Divisibility(double x, double y)
    {
        if (x % y == 0)
        { return true; }
        else
        { return false; }
    }
    Console.Clear();
    Console.Write("\n\n\n\n");
    Console.WriteLine("    ==== DIVISIBILIDADE DE UM NÚMERO ====\n");
    Console.Write("    Este algoritmo verifica se um número é divisível por outro.");
    Console.Write("\n    Por favor, digite o primeiro número: ");
    double first = int.Parse(Console.ReadLine()!);
    Console.Write("    Por favor, digite o segundo número: ");
    double second = int.Parse(Console.ReadLine()!);
    if (Divisibility(first, second) == true)
    { Console.WriteLine($"\n    {first} é divisível por {second}."); }
    else { Console.WriteLine($"\n    {first} não é divisível por {second}."); }
    Console.ReadLine();

}

// Exercício 05
void Exercise05()
{
    Console.Clear();
    Console.Write("\n\n\n\n");
    Console.WriteLine("    ==== PASSAGEM POR REFERÊNCIA ====\n");
    Console.Write("    Este algoritmo possibilita o arredondamento de um número real para um número" +
        "\n    inteiro, seguindo o arredondamento padrão, utilizando a passagem por referência. ");
    Console.ReadLine();
    Console.Write("\n    Qual é o número que deseja arredondar? ");
    double number = double.Parse(Console.ReadLine());
    Console.WriteLine($"\n      {number} => {Rounding(number)}");
    Console.ReadLine();
    int Rounding(double aNumber)
    {
        // Para se obter apenas a parte inteira do número, recorri a um "casting".
        // Para isso, procurei pela sintaxe de "como converter um tipo em outro" em C#;
        int anInteger = (int)aNumber;
        double aDecimal = aNumber - anInteger;
        if (aDecimal > 0.5)
        { return anInteger + 1; }
        else if (aDecimal == 0.5)
        {
            if (anInteger % 2 == 0)
            { return anInteger; }
            else
            { return anInteger + 1; }
        }
        else { return anInteger; }
    }
    Console.Clear();
}

// Exercício 09
void Exercise09()
{
    Console.Clear();
    int[] ArrayFilling(int size)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        { array[i] = random.Next(1, 200); }
        return array;
    }
    void ShowArray(int[] array)
    {
        foreach(int element in array) 
        { 
            Console.Write($"    {string.Join(",", element)}"); 
        }
    }
    int MultiplesOf7(int[] array)
    {
        int counter = 0;
        foreach(int element in array)
        {
            if(element % 7 == 0)
            {
                counter++;
            }
        }
        return counter;
    }
    int[] newArray = ArrayFilling(30);
    Console.WriteLine($"    Vetor original: ");
    ShowArray(newArray);
    Console.WriteLine($"    Há {MultiplesOf7(newArray)} múltiplos de 7 neste vetor;");
}


void Atividade03()
{
    int option;
    bool off = true;
    do
    {
        Console.Clear();
        Console.Write(@"



        Instituto Federal de Educação, Ciência e Tecnologia de Rondônia - IFRO
        C.S.T. em Análise e Desenvolvimento de Sistemas
        Disciplina de Algoritmo e Lógica de Programação II
        Professor Clayton Ferraz Andrade
        Acadêmico Vinicius de Oliveira Pinheiro

        Atividade n. 03 - Funções, Vetores e Matrizes

                Exercício [1] - Ordenador de números;
                Exercício [2] - Análise de matriz; 
                Exercício [3] - Conversor de moedas;
                Exercício [4] - Divisibilidade de um número;
                Exercício [5] - Passagem por referência;
                Exercício [9] - 

                Para sair, digite 0 ou -1 

        Digite qual exercício deseja verificar: ");
        option = int.Parse(Console.ReadLine()!);
        switch (option)
        {
            case 1 or 01: Exercise01(); break;
            case 2 or 02: Exercise02(); break;
            case 3 or 03: Exercise03(); break;
            case 4 or 04: Exercise04(); break;
            case 5 or 05: Exercise05(); break;
            case 9 or 09: Exercise09(); break;
            case 0 or -1: off = false; break;
            default: Console.Write("\n        Não entendi. Poderia repetir?"); break;
        }
    } while (off);
    Console.ReadKey();
}
Atividade03();
