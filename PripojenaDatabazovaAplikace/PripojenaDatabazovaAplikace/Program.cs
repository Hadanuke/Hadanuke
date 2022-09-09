using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PripojenaDatabazovaAplikace
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder sestavovac = new SqlConnectionStringBuilder();
            sestavovac.DataSource = @"C305-PC4\SQLEXPRESS";
            sestavovac.InitialCatalog = "Slovnicek";
            //sestavovac.IntegratedSecurity = true;
            sestavovac.UserID = "sa";
            sestavovac.Password = "sssaaa";

            Console.WriteLine(sestavovac.ConnectionString);

            using (SqlConnection pripojeni = new SqlConnection(sestavovac.ConnectionString))
            {
                pripojeni.Open();
                Console.WriteLine("Připojeno");
                SqlCommand dotaz = new SqlCommand();
                dotaz.Connection = pripojeni;
                
            while(true)
            {
                Console.WriteLine("Chcete pridat slovicko [1] , Vyhledat slovo [2] nebo smazat záznam dle ID [3] ?");
                int odpoved1 = int.Parse(Console.ReadLine());

                if (odpoved1 == 1)
                {

                    Console.WriteLine("Zadejte česky: ");
                    string cesky = Console.ReadLine();

                    Console.WriteLine("Zadejte anglicky: ");
                    string anglicky = Console.ReadLine();

                    dotaz.CommandText = "SELECT * FROM Kategorie";

                    Console.WriteLine("Kategorie jsou : ");

                    SqlDataReader ctecka = dotaz.ExecuteReader();

                    while (ctecka.Read())
                    {
                        Console.WriteLine("{0} {1} ", ctecka[0], ctecka[1]);
                    }
                    ctecka.Close();

                    Console.WriteLine("Jakou kategorii chcete?");
                    int kategorie = int.Parse(Console.ReadLine());
                    dotaz.CommandText = "SELECT * FROM Slovo";

                    dotaz.CommandText = "INSERT INTO Slovo(Cesky, Anglicky, Obtiznost, KategorieID) VALUES (@cesky, @anglicky, 1, @kategorie)";
                    dotaz.Parameters.AddWithValue("@cesky", cesky);
                    dotaz.Parameters.AddWithValue("@anglicky", anglicky);
                    dotaz.Parameters.AddWithValue("@kategorie", kategorie);
                    dotaz.ExecuteNonQuery();



                }
                if(odpoved1 == 2)
                {
                    Console.WriteLine("Chcete hledat s s ceskym [1] slovickem nebo anglickym [2] ?");
                    int odpoved2 = int.Parse(Console.ReadLine());
                    if(odpoved2 == 1)
                    {
                        Console.WriteLine("Zadejte slovo.");
                        string searchCesky = Console.ReadLine();
                        dotaz.CommandText = "SELECT * FROM Slovo";
                        SqlDataReader ctecka = dotaz.ExecuteReader();

                        while (ctecka.Read())
                        {
                            if(ctecka[1].ToString() == searchCesky)
                            {
                                Console.WriteLine("{0} {1} {2} ", ctecka[1], ctecka[2], ctecka[4]);
                            }
                        }
                        ctecka.Close();
                        Console.ReadLine();
                    }
                    if (odpoved2 == 2)
                    {

                        Console.WriteLine("Zadejte slovo.");
                        string searchAng = Console.ReadLine();
                        dotaz.CommandText = "SELECT * FROM Slovo";
                        SqlDataReader ctecka = dotaz.ExecuteReader();

                        while (ctecka.Read())
                        {
                            if (ctecka[2].ToString() == searchAng)
                            {
                                Console.WriteLine("{0} {1} {2} ", ctecka[1], ctecka[2], ctecka[4]);
                            }
                        }
                        ctecka.Close();
                        
                    }

                }
                if(odpoved1 == 3)
                {
                    Console.WriteLine("Zadejte ID");
                    string IdDelete = Console.ReadLine();

                    dotaz.CommandText = "DELETE FROM Slovo WHERE ID =@IdDelete";
                    dotaz.Parameters.AddWithValue("@IdDelete", IdDelete);
                    dotaz.ExecuteNonQuery();

                    Console.WriteLine("Deleted.");
                }

                
            }

                
               

                


                

                

                Console.ReadKey();
                pripojeni.Close();
            }
        }
    }
}
