#include <iostream>
#include <string>
#include <thread>
#include <chrono>

using namespace std;

void optionsDisplay();
long userInput(string method);
void closeWindow();

int main()
{
	optionsDisplay();
	return 0;
}

void optionsDisplay()
{
	string mark = "Taschenrechner von Corvin (ver. 1.0)\n--------------------------------------------\n";

	cout << mark;
	cout << "[ 1 ] ..........Addition";
	cout << "\n[ 2 ] .......Subtraktion";
	cout << "\n[ 3 ] ....Multiplikation";
	cout << "\n[ 4 ] ..........Division";
	cout << "\n[ 5 ] .....Modulo (Rest)\n";
	cout << "\nVerfahren waehlen (Zahl eingeben und Enter druecken): ";

	unsigned short calculation;
	cin >> calculation;

	switch (calculation) {
		case 1:
			system("cls");
			cout << mark << "Addition gewaehlt!\n";
			cout << "\nErgebnis: " << userInput("addition");
			closeWindow();
			break;
		case 2:
			system("cls");
			cout << mark << "Subtraktion gewaehlt!\n";
			cout << "\nErgebnis: " << userInput("subtraktion");
			closeWindow();
			break;
		case 3:
			system("cls");
			cout << mark << "Multiplikation gewaehlt!\n";
			cout << "\nErgebnis: " << userInput("multiplikation");
			closeWindow();
			break;
		case 4:
			system("cls");
			cout << mark << "Division gewaehlt!\n";
			cout << "\nErgebnis: " << userInput("division");
			closeWindow();
			break;
		case 5:
			system("cls");
			cout << mark << "Modulo (Rest) gewaehlt!\n";
			cout << "\nRest der Division: " << userInput("modulo");
			closeWindow();
			break;
		default:
			cout << "\nUngueltige Eingabe! Bitte nur eine der oben genannten Zahlen eingeben.";
			this_thread::sleep_for(chrono::seconds(4));
			system("cls");
			optionsDisplay();
			break;
	}
}

long userInput(string method)
{
	int number1 = 0;
	int number2 = 0;

	cout << "\nErste Zahl eingeben: ";
	cin >> number1;

	cout << "Zweite Zahl eingeben: ";
	cin >> number2;

	if (method == "addition") { 
		return number1 + number2;
	} else if (method == "subtraktion") {
		return number1 - number2;
	} else if (method == "multiplikation") {
		return number1 * number2;
	} else if (method == "division") {
		return number1 / number2;
	} else {
		return number1 % number2;
	}

	return 0;
}

void closeWindow()
{
	cout << "\n\n--------------------------------------------\nDas Programm wird nun beendet. Vielen Dank fuer Ihre Verwendung!";
	this_thread::sleep_for(chrono::seconds(5));
	exit(0);
}