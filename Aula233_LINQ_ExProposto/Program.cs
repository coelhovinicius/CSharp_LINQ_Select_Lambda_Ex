/* Fazer um programa para ler os dados (nome, email e salario) de funcionarios a partir de um arquivo em formato .csv. Em seguida,
 * mostrar, em ordem alfabética, o email dos funcionarios cujo salario seja superior a um dado valor fornecido pelo usuario.
 * Mostrar, tambem, a soma dos salarios dos funcionarios cujo nome comece com a letra 'M'.
 */

/* >>> PROGRAMA PRINCIPAL <<< */
using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Aula233_LINQ_ExProposto.Entities;

namespace Aula233_LINQ_ExProposto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: "); // C:\temp\Projetos\Aula233_LINQ_ExProposto\Employees.txt
            string path = Console.ReadLine();
            Console.Write("Enter salary to compare: ");
            double s = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            List<Employee> list = new List<Employee>();

            try
            {

                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] fields = sr.ReadLine().Split(',');
                        string name = fields[0];
                        string email = fields[1];
                        double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);
                        list.Add(new Employee(name, email, salary));
                    }

                    Console.WriteLine("Email of people whose salary is more than " + s.ToString("F2", CultureInfo.InvariantCulture));
                    var emails = list.Where(obj => obj.Salary > s).OrderBy(obj => obj.Email).Select(obj => obj.Email).DefaultIfEmpty();
                    foreach (string email in emails)
                    {
                        Console.WriteLine(email);
                    }

                    // Funcao para filtrar pessoas com nome iniciando com "M" e somar os salarios das mesmas
                    var sum = list.Where(obj => obj.Name[0] == 'M').DefaultIfEmpty().Sum(obj => obj.Salary);
                    Console.WriteLine("Sum of salary from people whose name starts with 'M': " + sum.ToString("F2", CultureInfo.InvariantCulture));
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error");
                Console.WriteLine(e.Message);
            }
        }
    }
}
