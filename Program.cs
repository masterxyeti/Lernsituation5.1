using System;
using System.IO;


// Temperaturauswertung
// - Array wird nicht benutzt, daher ohne "Split"-Funktion
// - DateTime wird nicht benutzt
// - Es wird der Einfachheit halber davon ausgegangen, dass jeder Datensatz das korrekte Format hat, daher erfolgt keine Fehlerbehandlung bei falschen Daten


namespace Temperaturauswertung
{
	class Program
	{
		public static void Main(string[] args)
		{


			string auswahl;
			double ergebnis;
			string datum;

			do
			{
				Console.Clear();
				Console.WriteLine("1 - Durchschnittstemperatur insgesamt ausgeben");
				Console.WriteLine("2 - Durchschnittstemperatur eines Tages ausgeben");
				Console.WriteLine("3 - Maximaltemperatur eines Tages ausgeben");
				Console.WriteLine();
				Console.WriteLine("0 - Programm beenden");
				Console.WriteLine();
				Console.Write("Ihre Wahl (0-3): ");
				auswahl = Console.ReadLine();

				Console.Clear();

				switch (auswahl)
				{
					case "1":
						ergebnis = GetDurchschnittstemperatur();
						Console.WriteLine("Die Durchschnittstemperatur insgesamt ist {0:F2} Grad", ergebnis);
						break;
					case "2":
						Console.Write("Bitte geben Sie das Datum in der Form JJJJ-MM-TT an: ");
						datum = Console.ReadLine();
						ergebnis = GetDurchschnittstemperatur(datum);
						Console.WriteLine("Die Durchschnittstemperatur am {0} ist {1:F2} Grad", datum, ergebnis);
						break;
					case "3":
						Console.Write("Bitte geben Sie das Datum in der Form JJJJ-MM-TT an: ");
						datum = Console.ReadLine();
						ergebnis = GetMaximaltemperatur(datum);
						Console.WriteLine("Die Maximaltemperatur am {0} ist {1:F2} Grad", datum, ergebnis);
						break;
					case "0":
						Console.WriteLine("Tschüss!");
						break;


				}

				Console.Write("Press any key to continue . . . ");
				Console.ReadKey(true);

			} while (auswahl != "0");



		}

		public static double GetDurchschnittstemperatur()
		{
			// Ermittlung der Durchschnittstemperatur über alle Werte in der Datei temperaturen.txt
			double durchschnittstemperatur = 0;
			double temperatur;
			int anzahlDaten = 0;
			string data; //Hilfsvariable 
			string datensatz;

			StreamReader sr = new StreamReader(@"C:\Users\tsengpiel\Desktop\temperaturen.txt");

			//Datensätze auslesen
			while (!sr.EndOfStream)
			{
				datensatz = sr.ReadLine();

				data = GetElement(datensatz, 3);
				temperatur = Convert.ToDouble(data);

				durchschnittstemperatur = durchschnittstemperatur + temperatur;
				anzahlDaten++;

			}

			sr.Close();

			durchschnittstemperatur = durchschnittstemperatur / anzahlDaten;
			return durchschnittstemperatur;
		}

		public static double GetDurchschnittstemperatur(string datum)
		{
			double durchschnittstemperatur = 0;
			double temperatur;
			int anzahlDaten = 0;
			string data; //Hilfsvariable 
			string datensatz;

			StreamReader sr = new StreamReader(@"C:\Users\tsengpiel\Desktop\temperaturen.txt");

			//Datensätze auslesen
			while (!sr.EndOfStream)
			{
				datensatz = sr.ReadLine();
				data = GetElement(datensatz, 0); //Datum auslesen
				if (data == datum)
				{
					data = GetElement(datensatz, 3);
					temperatur = Convert.ToDouble(data);

					durchschnittstemperatur = durchschnittstemperatur + temperatur;
					anzahlDaten++;
				}
			}

			sr.Close();

			durchschnittstemperatur = durchschnittstemperatur / anzahlDaten;
			return durchschnittstemperatur;
		}


		public static double GetMaximaltemperatur(string datum)
		{
			// Ermittlung des höchsten Temperaturwertes für ein bestimmtes Datum
			double maximaltemperatur = 0;
			double temperatur;
			string data; //Hilfsvariable 
			string datensatz;


			StreamReader sr = new StreamReader(@"C:\Users\tsengpiel\Desktop\temperaturen.txt");

			//Datensätze auslesen
			while (!sr.EndOfStream)
			{
				datensatz = sr.ReadLine();
				data = GetElement(datensatz, 0); //Datum auslesen
				if (data == datum)
				{
					data = GetElement(datensatz, 3);
					temperatur = Convert.ToDouble(data);
					if (maximaltemperatur < temperatur)
					{
						maximaltemperatur = temperatur;
					}

				}
			}

			sr.Close();


			return maximaltemperatur;
		}



		/// <summary>
		/// Diese Methode liefert ein Datenelement zurück.
		/// </summary>
		/// <param name="datensatz">Der korrekt formatierte Datensatz</param>
		/// <param name="index">Der Index des gesuchten Elements, beginnend mit Index 0</param>
		/// <returns>Das Element an der gesuchten Position</returns>
		public static string GetElement(string datensatz, int index)
		{
			string ergebnis;

			int aktuellerIndex = 0;
			int elementPosition = 0; //An dieser Position beginnt das Element
			int semikolonPosition = datensatz.IndexOf(';'); //Und dies ist die Position des Semikolons, welches das Ende des Elements kennzeichnet

			while (aktuellerIndex < index)
			{
				//Das nächste Element wird gesucht
				aktuellerIndex++;
				//Es beginnt direkt hinter dem zuletzt gefundenen Semikolon 
				elementPosition = semikolonPosition + 1;
				//Und wir suchen das nachfolgende Semikolon
				semikolonPosition = datensatz.IndexOf(';', elementPosition);
			}

			//Da hinter dem letzten Element kein Semikolon ist, muss der Fall separat behandelt werden
			if (semikolonPosition == -1) //Es wurde kein Semikolon gefunden
			{
				ergebnis = datensatz.Substring(elementPosition);
			}
			else
			{
				//Die Länge des Elements ist semikolonPosition - elementPosition
				ergebnis = datensatz.Substring(elementPosition, semikolonPosition - elementPosition);
			}
			return ergebnis;
		}



	}



}
