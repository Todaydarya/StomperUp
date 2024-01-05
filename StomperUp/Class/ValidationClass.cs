using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomperUp.Class
{
    internal class ValidationClass
    {
        #region Проверка на длину символов
        public bool ValidateStringLength(string input, int maxLength)
        {
            if (input == null)
            {
                // Можно добавить логику для обработки случая, когда входная строка равна null
                return false;
            }

            return input.Length <= maxLength;
        }
        //Вызов метода на символы
        /*StringValidator validator = new StringValidator();

        string exampleString = "Hello, World!";
        int maxLength = 10;

        bool isValid = validator.ValidateStringLength(exampleString, maxLength);

        if (isValid)
        {
            Console.WriteLine("Строка прошла проверку длины.");
        }
        else
        {
            Console.WriteLine("Строка не соответствует заданной длине.");
        }*/
        #endregion

        #region Проверка на ввод только цифр
        public bool ContainsOnlyDigits(string input)
        {
            if (input == null)
            {
                // Можно добавить логику для обработки случая, когда входная строка равна null
                return false;
            }

            return input.All(char.IsDigit);
        }

        //Вызов метода
        /*StringValidator validator = new StringValidator();

                string exampleString = "12345";

                bool containsOnlyDigits = validator.ContainsOnlyDigits(exampleString);

                if (containsOnlyDigits)
                {
                    Console.WriteLine("Строка содержит только цифры.");
                }
                else
                {
                    Console.WriteLine("Строка содержит символы, отличные от цифр.");
                }*/
        #endregion

        #region Проверка на английские буквы
        public bool ContainsOnlyEnglishLettersAndAt(string input)
        {
            if (input == null)
            {
                // Можно добавить логику для обработки случая, когда входная строка равна null
                return false;
            }

            // Условие для проверки, что все символы являются английскими буквами или "@"
            return input.All(c => char.IsLetter(c) || c == '@');
        }
        #endregion
    }
}
