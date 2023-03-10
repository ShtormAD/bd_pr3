using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pr3
{
    class DBUtils
    {
        private static String database;
        //Создаем подключение
        public static MySqlConnection GetDBConnection()
        {
            try
            {
                string host = "shtormad.keenetic.pro";
                int port = 2205;
                database = "mydb";
                string username = "remote_user";
                string password = "remUsrPass";

                String connString = "Server=" + host + ";Database=" + database
                    + ";port=" + port + ";User Id=" + username + ";password=" + password;

                MySqlConnection conn = new MySqlConnection(connString);

                return conn;
            } catch (Exception e)
            {
                showDial();
                return null;
            }
            
        }

        //Получение списка таблиц
        public static String[] getTables(MySqlConnection conn)
        {
            List<string> res = new List<string>();
            try
            {
                //Получаем из БД список таблиц
                String query = "show tables where Tables_in_"+ database + " not rlike 'lang';";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                //Перегоняем в массив и возвращаем
                while (reader.Read())
                {
                    res.Add(reader.GetString("Tables_in_"+database));
                }
                reader.Close();
                return res.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine("getTables");
                Console.WriteLine(e.ToString());
                showDial();
                return res.ToArray();
            }
        }
        public static String[] getTablesLang(MySqlConnection conn)
        {
            List<string> res = new List<string>();
            try
            {
                //Получаем из БД список таблиц
                String query = "select * from tables_lang;";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                //Перегоняем в массив и возвращаем
                while (reader.Read())
                {
                    res.Add(reader.GetString("table_lang"));
                }
                reader.Close();
                return res.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine("getTablesLang");
                Console.WriteLine(e.ToString());
                showDial();
                return res.ToArray();
            }
        }

        //Выборка данных из таблицы
        public static List<String[]> LoadData(MySqlConnection conn, String table)
        {
            List<string[]> data = new List<string[]>();
            try
            {
                //Запрашиваем * из таблицы
                string query = "SELECT * FROM " + table + ";";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();

                //Заполняем массив данными
                int c = reader.FieldCount;
                data.Add(new string[c]);
                data.Add(new string[c]);
                while (reader.Read())
                {
                    data.Add(new string[c]);
                    for (int i = 0; i < c; i++)
                    {
                        //Проверяем ячейку на наличие даты, если да - форматируем под MySQL
                        if (DateTime.TryParse(reader.GetString(i), out DateTime dt))
                            data[data.Count - 1][i] = reader.GetDateTime(i).ToString("yyyy-MM-dd");
                        else
                            data[data.Count - 1][i] = reader.GetString(i);
                    }
                }
                //Заполняем название колонок
                for (int i = 0; i < c; i++)
                {
                    data[1][i] = reader.GetName(i).ToString();
                }
                reader.Close();

                query = "SELECT col_name_lang FROM " + table + "_lang;";
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
                int it = 0;
                while (reader.Read())
                {
                    data[0][it] = reader.GetString(0);
                    it++;
                }
                reader.Close();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("LoadData");
                Console.WriteLine(e.Message);
                showDial();
                return data;
            }
        }
        public static List<String[]> LoadData(MySqlConnection conn, String table, String rl)
        {
            List<string[]> data = new List<string[]>();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                //Запрашиваем * из таблицы
                string query = "SELECT * FROM " + table + " where full_name like '"+rl+"%';";
                cmd.CommandText = query;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();

                //Заполняем массив данными
                int c = reader.FieldCount;
                data.Add(new string[c]);
                data.Add(new string[c]);
                while (reader.Read())
                {
                    data.Add(new string[c]);
                    for (int i = 0; i < c; i++)
                    {
                        //Проверяем ячейку на наличие даты, если да - форматируем под MySQL
                        if (DateTime.TryParse(reader.GetString(i), out DateTime dt))
                            data[data.Count - 1][i] = reader.GetDateTime(i).ToString("yyyy-MM-dd");
                        else
                            data[data.Count - 1][i] = reader.GetString(i);
                    }
                }
                //Заполняем название колонок
                for (int i = 0; i < c; i++)
                {
                    data[1][i] = reader.GetName(i).ToString();
                }
                reader.Close();

                query = "SELECT col_name_lang FROM " + table + "_lang;";
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
                int it = 0;
                while (reader.Read())
                {
                    data[0][it] = reader.GetString(0);
                    it++;
                }
                reader.Close();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("LoadData");
                Console.WriteLine(e.Message);
                showDial();
                return data;
            }
        }

        //Удаление записи
        public static void delRow(MySqlConnection conn,int id, String tabl, String colN)
        {
            try
            {
                //Ну тут все просто, собираем запрос и проводим
                String query = "DELETE FROM " + tabl + " WHERE " + colN + " = " + id + ";";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                showDial();
            }
        }

        //Добавление записи
        public static void addRow(MySqlConnection conn, String[] cols, String[] vals, String tabl)
        {
            try
            {
                //Собираем запрос из переданных колонок и значений
                String query = "Insert into " + tabl + " (" + cols[0];
                for (int i = 1; i < cols.Length; i++)
                    query += ", " + cols[i];
                query += ") values (" + per(vals[0]);
                for (int i = 1; i < vals.Length; i++)
                    query += ", " + per(vals[i]);
                query += ");";

                //И выполняем его
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                showDial();
            }
        }

        //Редактирование записей
        public static void editRow(MySqlConnection conn, String[] cols, String[] vals, String tabl, String id_col, int id)
        {
            try
            {
                //Собираем запрос из переданных аргументов
                String query = "update " + tabl + " set " + cols[0] + " = " + per(vals[0]);

                for (int i = 1; i < cols.Length; i++)
                    query += ", " + cols[i] + " = " + per(vals[i]);

                query += " where " + id_col + " = " + id;

                //И выполняем его
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            } catch(Exception e)
            {
                showDial();
            }
            
        }

        //Строки и даты в синтаксисе MySQL 'обносятся', если не цифра - получай одинарные кавычки 
        private static String per(String s)
        {
            if (int.TryParse(s, out int i))
                return s;
            else return "'"+s+"'";
        }

        //Метод диалога, для предупреждения об ошибках
        private static void showDial()
        {
            MessageBox.Show(
                    "Произошла ошибка при работе с БД. Проверьте корректность данных и доступность БД.",
                    "Внимание!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
        }

        internal static List<string[]> LoadExtr(MySqlConnection conn)
        {
            List<string[]> data = new List<string[]>();
            try
            {
                //Запрашиваем * из таблицы
                string query = "SELECT mydb.exttraditions.id_ext as 'ID', mydb.exttraditions.date as 'Дата выдачи', " +
	                                "(select full_name from mydb.students where id_student = exttraditions.students_id_student) as 'Студент', "+
                                    "(select full_name from mydb.employers where id_employee = exttraditions.employers_id_employee) as 'Сотрудник',"+
                                    "(select date from mydb.book where id_book = exttraditions.book_id_book) as 'Книга' from mydb.exttraditions;";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();

                //Заполняем массив данными
                int c = reader.FieldCount;
                data.Add(new string[c]);
                while (reader.Read())
                {
                    data.Add(new string[c]);
                    for (int i = 0; i < c; i++)
                    {
                        //Проверяем ячейку на наличие даты, если да - форматируем под MySQL
                        if (DateTime.TryParse(reader.GetString(i), out DateTime dt))
                            data[data.Count - 1][i] = reader.GetDateTime(i).ToString("yyyy-MM-dd");
                        else
                            data[data.Count - 1][i] = reader.GetString(i);
                        Console.WriteLine(reader.GetString(i));
                    }
                }
                //Заполняем название колонок
                for (int i = 0; i < c; i++)
                {
                    data[0][i] = reader.GetName(i).ToString();
                }
                reader.Close();
                return data;
            }
            catch (Exception e)
            {
                showDial();
                return data;
            }
        }

        public static List<String[]> LoadStudCount(MySqlConnection conn, String group)
        {
            List<string[]> data = new List<string[]>();
            try
            {
                //Запрашиваем * из таблицы
                string query = "select students.full_name as 'Студент', count(*) as 'Взял книг' from exttraditions, students where students_id_student in (select id_student from students where groups_id_group = (select id_group from groupss where group_name like '" + group + "%')) " +
                                    "and students.id_student in (select id_student from students where groups_id_group = (select id_group from groupss where group_name like '" + group + "%'));";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();

                //Заполняем массив данными
                int c = reader.FieldCount;
                data.Add(new string[c]);
                data.Add(new string[c]);
                while (reader.Read())
                {
                    data.Add(new string[c]);
                    for (int i = 0; i < c; i++)
                    {
                        //Проверяем ячейку на наличие даты, если да - форматируем под MySQL
                        if (DateTime.TryParse(reader.GetString(i), out DateTime dt))
                            data[data.Count - 1][i] = reader.GetDateTime(i).ToString("yyyy-MM-dd");
                        else
                            data[data.Count - 1][i] = reader.GetString(i);
                        Console.WriteLine(reader.GetString(i));
                    }
                }
                //Заполняем название колонок
                for (int i = 0; i < c; i++)
                {
                    data[0][i] = reader.GetName(i).ToString();
                }
                reader.Close();
                return data;
            }
            catch (Exception e)
            {
                 Console.WriteLine(e.Message);
                showDial();
                return data;
            }
        }
    }
}
