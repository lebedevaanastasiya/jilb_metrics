using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;


namespace msis_lab2_Jilba
{
    class Program
    {
        static string pattern;
        static Regex regex;
        static Match match;
        static string regular_for_if = @"([^(\bend\b)]\s+\bif\b)+";
        static string regular_for_endif = @"(\bend\b\s+\bif\b)+";
        static string regular_for_select = @"([^end]\s+\bSelect\b)+";
        static string raw_text_of_program = "";
        static int absolute_complexity = 0;
        static int number_of_operators = 0;
        static int maximum_nest_of_conditional_statement = 0;

        static void Main(string[] args)
        {
            string is_program_continuation = "1";
            string path_of_program_file;
           
            float relative_complexity;

            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine("\n1 - > Evaluation of the program by using Djilb\'s metric");
            Console.WriteLine("0 - > Exit");
            is_program_continuation = Console.ReadLine();
            while (is_program_continuation == "1")
            {
                Console.WriteLine("Enter the name of file:");
                path_of_program_file = @"..\" + @Console.ReadLine();
                Console.WriteLine("\n***\n");
                try
                {
                    raw_text_of_program = ReadProgramText(path_of_program_file);
                    Console.WriteLine(raw_text_of_program);
                    Console.WriteLine("\n***\n");
                    raw_text_of_program = DeleteCommentsAndStrings(raw_text_of_program);
                    Console.WriteLine(raw_text_of_program);


                    Console.WriteLine("\n***\n");
                    absolute_complexity = DefineNumberOfConditionalStatements(raw_text_of_program);
                    number_of_operators = DefineNumberOfOperators(raw_text_of_program);
                    Console.WriteLine("Абсолютная сложность программы: {0}", absolute_complexity);
                    relative_complexity = (absolute_complexity * (float)1.0) / number_of_operators;
                    Console.WriteLine("Относительная сложность программы: {0}", relative_complexity);
                    CalculateMaximumNest(raw_text_of_program);
                    Console.WriteLine("максимальный уровень вложенности: {0}", maximum_nest_of_conditional_statement);
                    Console.WriteLine("\n***\n");

                }
                catch (Exception e)
                {
                    Console.WriteLine("\nFile is not found! Try again!\n");
                }
                Console.WriteLine("\nWhat do you want to do?");
                Console.WriteLine("\n1 - > Evaluation of the program by using Djilb\'s metric");
                Console.WriteLine("0 - > Exit");
                is_program_continuation = Console.ReadLine();
                absolute_complexity = 0;
                maximum_nest_of_conditional_statement = 0;
                relative_complexity = 0;
            }
            Console.WriteLine("Bye! :)");
            Console.ReadLine();
        }

        static string ReadProgramText(string path_of_program_file)
        {
            string program_text;
            StreamReader file = File.OpenText(path_of_program_file);
            program_text = file.ReadToEnd();
            file.Close();
            return program_text;
        }

        static string DeleteCommentsAndStrings(string text)
        {
            int state;
            const int
                in_comment = 1,
                in_string = 2,
                in_program = 0;
            const int
                length_of_comment_left_border = 1;
            int comments_start, comments_finish;

            state = in_program;
            int i = 0;
            while (i < text.Length - 1)
            {
                comments_start = 0;
                comments_finish = 0;
                if (text[i] == '\'')
                    state = in_comment;               
                if (text[i] == '"')
                    state = in_string;

                switch (state)
                {
                    case in_comment:
                        comments_start = i;
                        for (int k = comments_start + length_of_comment_left_border; 
                        (k < text.Length) && comments_finish == 0; k++)
                            if (text[k] == '\n')
                                comments_finish = k;
                        text = text.Remove(comments_start, comments_finish - comments_start);
                        state = in_program;
                        break;                    
                    case in_string:
                        i++;
                        while (text[i] != '"')
                        {
                            text = text.Remove(i, 1);
                        }
                        i++;
                        state = in_program;
                        break;
                    case in_program: i++;
                        break;
                }
            }
            return text;
        }

        static int DefineNumberOfConditionalStatements(string  program_text)
        {
            int count_of_if = 0;
            int count_of_select = 0;

            SearchOutCoincidence(regular_for_if,program_text);
            while (match.Success)
            {
                count_of_if++;
                match = match.NextMatch();
            }
            SearchOutCoincidence(regular_for_select, program_text);
            while (match.Success)
            {
                count_of_select++;
                match = match.NextMatch();
            }
            return count_of_if + count_of_select;

        }

        static int DefineNumberOfOperators(string  program_text)
        {
            int number_of_operators = 0;
            string regular_for_operators = @"";
            string regular_of_part1 = @"(\^|\*|\\|\/|\bmod\b|\+|-|<|>|=|<>|<=|>=|>>|<<|-=|\+=|\*=|\=|\/=|\^=|:=|\&=|<<=|>>=|";
            string regular_of_part2 = @"\bis\b|\bisnot\b|\blike\b|&|\band\b|\bandalso\b|\beqv\b|\bimp\b|\bnot\b|\bor\b|\borelse\b|\bxor\b|";
            string regular_of_part3 = @"\bsub\b|\bend sub\b|\bclass\b|\bend class\b|\bconst\b|\bdim\b|\bif\b|\bthen\b|\belse\b|\bfor\b|\bnext\b|,|\(|\)|{|}|";
            string regular_of_part4 = @"\bselect\b|\bend select\b|\bnew\b){1}";

            regular_for_operators = regular_for_operators 
                                    + regular_of_part1 + regular_of_part2 
                                    + regular_of_part3 + regular_of_part4;
            SearchOutCoincidence(regular_for_operators, program_text);
            while (match.Success)
            {
                number_of_operators++;
                match = match.NextMatch();
            }
            return number_of_operators;
        }

        static void SearchOutCoincidence(string regular_expression,string text_for_searching)
        {            
            pattern = regular_expression;
            regex = new Regex(pattern, RegexOptions.IgnoreCase);
            match = regex.Match(text_for_searching);
        }

        static void CalculateMaximumNest(string text_of_program)
        {
            string start_of_select = "select";
            string end_of_select ="end select";
            string end_of_if = "end if";
            string start_of_if = "if";
            int number_of_if = 0;
            int temp_nest = 0;

            int i=1;
            while(i<text_of_program.Length)
            {
                if ((text_of_program.Length-i >= start_of_if.Length)
                    && (text_of_program.Substring(i, start_of_if.Length).ToLower()== start_of_if)  
                    && (text_of_program.Length >=i+start_of_if.Length+1
                    && (text_of_program[i + start_of_if.Length] == ' ' || text_of_program[i + start_of_if.Length] == '\t'
                    || text_of_program[i + start_of_if.Length] == '\r' || text_of_program[i + start_of_if.Length] == '\n'))
                    && (text_of_program[i - 1] == ' ' || text_of_program[i - 1] == '\t'
                    || text_of_program[i - 1] == '\r' || text_of_program[i - 1] == '\n') )
                {
                    number_of_if++;
                    i+=start_of_if.Length;
                }
                else
                {
                    if((text_of_program.Length-i>=start_of_select.Length)
                    && (text_of_program.Substring(i, start_of_select.Length).ToLower() == start_of_select)
                    && (text_of_program.Length >=i+start_of_select.Length+1
                    && (text_of_program[i + start_of_select.Length] == ' ' || text_of_program[i + start_of_select.Length] == '\t'
                    || text_of_program[i + start_of_select.Length] == '\r' || text_of_program[i + start_of_select.Length] == '\n'))
                    && (text_of_program[i - 1] == ' ' || text_of_program[i - 1] == '\t'
                    || text_of_program[i - 1] == '\r' || text_of_program[i - 1] == '\n') )
                    {
                        number_of_if++;
                        i += start_of_select.Length;
                    }
                    else
                    {
                        if ((text_of_program.Length - i >= end_of_if.Length)
                        && (text_of_program.Substring(i, end_of_if.Length).ToLower() == end_of_if)
                        && (text_of_program.Length >= i + end_of_if.Length + 1
                        && (text_of_program[i + end_of_if.Length] == ' ' || text_of_program[i + end_of_if.Length] == '\t'
                        || text_of_program[i + end_of_if.Length] == '\r' || text_of_program[i + end_of_if.Length] == '\n'))
                        && (text_of_program[i-1] == ' ' || text_of_program[i-1] == '\t'
                        || text_of_program[i-1] == '\r' || text_of_program[i-1] == '\n') )
                        {
                            if (temp_nest == 0)
                            {
                                temp_nest = number_of_if;                                
                            }
                            i += end_of_if.Length;
                            number_of_if--;
                        }
                        else
                        {
                            if ((text_of_program.Length - i >= end_of_select.Length)
                            && (text_of_program.Substring(i, end_of_select.Length).ToLower() == end_of_select)
                            && (text_of_program.Length >= i + end_of_select.Length + 1
                            && (text_of_program[i + end_of_select.Length] == ' ' || text_of_program[i + end_of_select.Length] == '\t'
                            || text_of_program[i + end_of_select.Length] == '\r' || text_of_program[i + end_of_select.Length] == '\n'))
                            && (text_of_program[i-1] == ' ' || text_of_program[i-1] == '\t'
                            || text_of_program[i - 1] == '\r' || text_of_program[i - 1] == '\n'))
                            {
                                temp_nest = number_of_if;
                                i += end_of_select.Length;
                                number_of_if--;
                            }
                        }                        
                    }
                }
                if (number_of_if == 0)
                {
                    if (temp_nest > maximum_nest_of_conditional_statement)
                        maximum_nest_of_conditional_statement = temp_nest;
                    temp_nest = 0;
                }
                i++;
            }
        }
    }
}
