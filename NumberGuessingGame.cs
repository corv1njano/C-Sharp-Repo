using System;

class MeineKlasse
{
    public static void Main(string[] args)
    {
        const int zahl = 6;
        int versuche = 0;
        bool programmLaeuft = true;

        Console.WriteLine("Rate eine Zahl zwischen 1 und 10!");

        while (programmLaeuft)
        {
            try
            {
                int input = Convert.ToInt32(Console.ReadLine());

                if (input == zahl)
                {
                    Console.WriteLine("Richtig, die Zahl war " + zahl + "!");
                    programmLaeuft = false;
                    // break;
                }
                else
                {
                    Console.WriteLine("Falsch, die Zahl war leider nicht " + input + ".\n");
                    Console.WriteLine("Versuche es nochmal! Rate die Zahl zwischen 1 und 10!");
                    versuche++;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nUngültge Eingabe! Bitte nur Zahlen-Werte eingeben! Versuche es nochmal.\nRate eine Zahl zwischen 1 und 10");
            }
        }

        if (versuche == 0)
        {
            Console.WriteLine("\n\nVielen Dank für Spielen! Du hast die Zahl schon beim ersten Mal richtig geraten!");
        }
        else
        {
            Console.WriteLine("\n\nVielen Dank für Spielen! Du hast " + versuche + " extra Versuche gebraucht.");
        }
    }
}
