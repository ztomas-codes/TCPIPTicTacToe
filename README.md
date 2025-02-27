# TCP/IP Piškvorky

**TCP/IP Piškvorky** je aplikace vyvinutá v jazyce C# s využitím Windows Forms, která umožňuje dvěma hráčům hrát hru piškvorky přes síť pomocí protokolu TCP/IP.

## 📂 Struktura projektu

Projekt je rozdělen do následujících hlavních složek:

- **ClientForms**: Obsahuje formuláře a třídy pro klientskou část aplikace.
- **Server**: Obsahuje třídy a logiku pro serverovou část aplikace.
- **TicTacToe**: Obsahuje společné třídy a logiku pro hru piškvorky.

## 🛠️ Požadavky

- Visual Studio s podporou C# a Windows Forms
- .NET 6.0

## 🚀 Spuštění aplikace

1. **Klientská aplikace**:
   - Otevřete řešení `TicTacToe.sln` ve Visual Studiu.
   - Nastavte projekt `ClientForms` jako spouštěný projekt.
   - Spusťte aplikaci (F5).
   - Zadejte IP adresu a port serveru, ke kterému se chcete připojit.

2. **Serverová aplikace**:
   - Otevřete řešení `TicTacToe.sln` ve Visual Studiu.
   - Nastavte projekt `Server` jako spouštěný projekt.
   - Spusťte aplikaci (F5).
   - Server začne naslouchat na předdefinovaném portu pro příchozí připojení.

## 🎮 Pravidla hry

- Hra je určena pro dva hráče.
- Hráči se střídají v umisťování svých symbolů (křížek nebo kolečko) na herní mřížku 3x3.
- Cílem je umístit tři své symboly v řadě, sloupci nebo diagonále.
