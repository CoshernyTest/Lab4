
int[,] initial_field = new int[4,4];
List<string> move_dirs = new List<string>();
string path_in = "input.txt";
string path_out = "output.txt";

#region ЧТЕНИЕ/ПОЛУЧЕНИЕ ИНФОРМАЦИИ
using (StreamReader sr = new StreamReader(path_in, System.Text.Encoding.Default))
{
    // Читаем 4х4 поле (т.е. только цифры)
    for (int y = 0; y < 4; y++)
    {
        string line = sr.ReadLine();

        // Разбиваем строку на массив int и присваиваем значения цифрам под соответствующими координатами в матрице initial_field
        int[] line_nums_array = Array.ConvertAll(line.Split(' '), s => int.Parse(s));
        for (int x = 0; x < 4; x++)
        {
            initial_field[x, y] = line_nums_array[x];
        }
    }

    // Получаем массив с направлениями движений
    move_dirs.AddRange(sr.ReadLine().Split(" "));

}
#endregion

#region ОБРАБОТКА ИНФОРМАЦИИ

ShiftableField field = new ShiftableField(initial_field);
//Console.WriteLine("START FIELD:\n" + field.GetMatrixInText(field.Field));
field.Field = field.ShiftFieldInDirections(field.Field, move_dirs);
//Console.WriteLine('\n');

#endregion 

#region ЗАПИСЬ/ВЫВОД
using (StreamWriter sw = new StreamWriter(path_out, false, System.Text.Encoding.Default))
{
    string matrix_in_text = Converter.GetMatrixInText(field.Field);
    
    //Console.WriteLine("FINAL:\n" + matrix_in_text);
    sw.Write(matrix_in_text);
}
#endregion


#region МЕТОДЫ

public class ShiftableField
{
    private int[,] field;
    public int[,] Field { get => field; set => field = value; }

    public ShiftableField() { field = new int[4, 4]; }
    public ShiftableField(int[,] _field)
    { field = _field; }
    
    //
    // Выполняет ряд сдвигов матрицы
    //
    public int[,] ShiftFieldInDirections(int[,] field, List<string> directions)
    {
        int[,] new_field = field;

        foreach (string dir in directions)
        {
            new_field = MoveFieldInDir(field, dir);
            //Console.WriteLine(dir + "\n" + GetMatrixInText(field.Field));
        }

        return new_field;
    }


    //
    // Сдвигает всю матрицу
    //
    public int[,] MoveFieldInDir(int[,] field, string dir)
    {
        int[,] new_field = field;

        switch (dir)
        {
            case "L":
            case "R":
                for (int y = 0; y < 4; y++)
                {
                    int[] line = { field[0, y], field[1, y], field[2, y], field[3, y] };  // Собираем ряд чисел
                    int[] new_line = ShiftLineInDirection(line, dir);     // Сдвигаем

                    for (int x = 0; x < 4; x++) { new_field[x, y] = new_line[x]; } // Присваем обновляем новое поле
                }
                break;
            case "U":
            case "D":
                for (int x = 0; x < 4; x++)
                {
                    int[] line = { field[x, 0], field[x, 1], field[x, 2], field[x, 3] };  // Собираем ряд чисел
                    int[] new_line = ShiftLineInDirection(line, dir);     // Сдвигаем

                    for (int y = 0; y < 4; y++) { new_field[x, y] = new_line[y]; } // Присваем обновляем новое поле
                }
                break;
        }

        return new_field;
    }

    //
    // Сдвигает линию и добавляет 0 с нужной стороны
    //
    public int[] ShiftLineInDirection(int[] line, string dir)
    {
        List<int> result = ShiftLine(line);

        if (result.Count < 4)
        {
            int zeros = 4 - result.Count;
            switch (dir)
            {
                case "L":   // Left
                case "U":   // Up
                    for (int i = 0; i < zeros; i++) { result.Add(0); }
                    break;

                case "R":   // Right
                case "D":   // Down
                    for (int i = 0; i < zeros; i++) { result.Insert(0, 0); }
                    break;
            }
        }

        return result.ToArray();
    }

    //
    // Непосредственно сам сдвиг линии
    //
    public List<int> ShiftLine(int[] line)
    {
        List<int> line_list = new List<int>(line);
        List<int> result = new List<int>();

        line_list.RemoveAll(x => x == 0);

        if (line_list.Count > 0)
        {
            if (line_list.Count == 1) { return line_list; }
            else
            {
                for (int i = 0; i < line_list.Count - 1; i++)
                {
                    if (line_list[i] == line_list[i + 1])
                    {
                        result.Add(line_list[i] * 2);
                        i += 1;
                    }
                    else
                    {
                        result.Add(line_list[i]);
                        if (i == line_list.Count - 2) { result.Add(line_list[i + 1]); }
                    }
                }
            }
        }


        return result;
    }
}

public class Converter
{
    public static int[,] GetTextToShiftableFieldField(string[] lines)
    {
        int[,] result = new int[4, 4];

        for (int y = 0; y < 4; y++)
        {
            // Разбиваем строку на массив int и присваиваем значения цифрам под соответствующими координатами в матрице initial_field
            int[] line_nums_array = Array.ConvertAll(lines[y].Split(' '), s => int.Parse(s));
            for (int x = 0; x < 4; x++)
            {
                result[x, y] = line_nums_array[x];
            }
        }

        return result;
    }

    public static string GetMatrixInText(int[,] field)
    // Превращает матрицу int в одну понятную string
    {
        string text = "";

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                text += field[x, y].ToString();
                if (x != 3) { text += " "; }
            }

            if (y != 3) { text += "\r\n"; }
        }

        return text;
    }
}
#endregion