using System;
using System.IO;
using NLog.Web;

namespace Movies
{
    class Program
    {
        static void Main(string[] args)
        {
             string path = Directory.GetCurrentDirectory() + "\\nlog.config";

            // create instance of Logger
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            // log sample messages
            // logger.Trace("Sample trace message");
            // logger.Debug("Sample debug message");
            // logger.Info("Sample informational message");
            // logger.Warn("Sample warning message");
            // logger.Error("Sample error message");
            // logger.Fatal("Sample fatal error message");
            
            String file = "movies.csv";
            
            Console.WriteLine("Enter 1 to Search a Movie");
            Console.WriteLine("Enter 2 to Search All Movies");
            Console.WriteLine("Enter 3 to Add a Movie");
            int selection;
            if (!int.TryParse(Console.ReadLine(), out selection)){
                logger.Error("Invalid Entry is not a number");
            }

            if (selection < 1 || selection > 3){
                logger.Error("Result invalid, Entry needs to be 1-3");
            }
            if (selection == 1)
            {
                StreamReader sr = new StreamReader(file);
                if (File.Exists(file))
                {
                    Console.WriteLine("Enter the Name of the Movie to Search");
                    String movie = Console.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();

                        String[] movieInfo = line.Split(",");

                        foreach (String m in movieInfo)
                        {
                            if (m.Contains(movie))
                            {
                                Console.WriteLine(line);
                            }
                        }

                    }

                }else{
                    logger.Info("File does not Exist");
                }
                sr.Close();
            }

            if (selection == 2)
            {
                StreamReader sr = new StreamReader(file);
                if (File.Exists(file))
                {
                    while (!sr.EndOfStream)
                    {

                        String line = sr.ReadLine();
                        Console.WriteLine(line);
                    }

                }else{
                    logger.Info("File does not Exist");
                }
                sr.Close();
            }

            if (selection == 3)
            {
                Boolean movieExists = false;
                String line = "";
                String[] movieArray = new string[3];
                String id = "";
                int intId = 0;
                Console.WriteLine("Enter a Movie Title");
                String movie = Console.ReadLine();

                Console.WriteLine("Enter the Movie year");
                String year = Console.ReadLine();

                String fullMovieTitle = movie + " (" + year + ")";

                StreamReader re = new StreamReader(file);
                if(File.Exists(file)){
                while (!re.EndOfStream)
                {
                    line = re.ReadLine();
                    String[] movieInfo = line.Split(",");
                    id = movieInfo[0];

                    if (movieInfo[1].Contains(fullMovieTitle))
                    {
                        logger.Info("This movie already exists");
                        Console.WriteLine(line);
                        movieExists = true;
                    }

                }}else{
                    logger.Info("File does not Exist");
                }
                Boolean isInt = Int32.TryParse(id, out intId);
                if (!isInt)
                {
                    logger.Error("Id is not a number");
                }
                movieArray[0] = (intId + 1).ToString();
                re.Close();
                // create new Id number
                
                if (movieExists == false)
                {
                    String[] genres = new string[10];

                    String cont = "y";
                    int count = 0;

                    while (cont == "y")
                    {

                        Console.WriteLine("Enter Genre");
                        String genre = Console.ReadLine();
                        genres[count] = genre;

                        // do{
                        Console.WriteLine("Would you like to add another genre? Y/N");
                        cont = Console.ReadLine().ToLower();
                        Console.WriteLine(cont);
                        // if(!cont.Equals("y") | !cont.Equals("n")){
                        //     logger.Trace("Invalid Entry");
                        // }
                        // }while(!cont.Equals("y") | !cont.Equals("n"));
                        count++;

                    }

                    String allGenre = genres[0];

                    for (var i = 1; i < count; i++)
                    {
                        allGenre += "|" + genres[i];
                    }


                    movieArray[1] = fullMovieTitle;
                    movieArray[2] = allGenre;

                    Console.WriteLine("Is this information correct?");
                    Console.WriteLine("{0},{1},{2}", movieArray[0], movieArray[1], movieArray[2]);
                    String yes = Console.ReadLine().ToLower();
                    
                    if (yes == "y"){
                    StreamWriter sw = new StreamWriter(file, true);
                   
                    sw.WriteLine("{0},{1},{2}", movieArray[0], movieArray[1], movieArray[2]);
                    sw.Close();
                    }
                }

                logger.Info("Program ended");
            }
           
        }
    }
}
