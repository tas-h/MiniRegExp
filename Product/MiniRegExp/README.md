# MiniRegExp

## A program m�k�d�se

A program a MiniRegExp.exe f�jl seg�ts�g�vel ind�that� el �s a jobb fels� sarokban l�v� k�k k�rlap seg�ts�g�vel �ll�that� le.  

A program els� ind�t�sa alkalm�val a k�vetkez� kezd� ablak jelenik meg, kit�ltetlen mez�kkel:  

![Kezd� k�perny�](../../Doc/MainWindow.png)  

A program lehet�s�get biztos�t:  
- regul�ris minta alap� keres�sre, l�sd: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.matches?view=net-6.0  
- �s cser�re, l�sd: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.replace?view=net-6.0  
egyar�nt.  

### Opci�k

- IgnoreCase - kis �s nagy bet�k megk�l�nb�ztet�se. Tov�bbi inf� itt: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regexoptions?view=net-6.0#System_Text_RegularExpressions_RegexOptions_IgnoreCase  
- Multiline - t�bb soros m�d. Tov�bbi inf� itt: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regexoptions?view=net-6.0#System_Text_RegularExpressions_RegexOptions_Multiline  
- Singleline - egy soros m�d. Tov�bbi inf� itt: https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regexoptions?view=net-6.0#System_Text_RegularExpressions_RegexOptions_Singleline  

### Keres�s

V�lasszuk ki a leny�l� list�ban a Match lehet�s�get, t�lts�k ki a keres�si mint�t a 'Regular Expression' mez�be, adjuk meg a bemeneti sz�veget, amiben keresni szeretn�nk az 'Input Text' mez�be, majd 
ind�tsuk el a keres�st a 'Start' gomb seg�ts�g�vel. Ennek hat�rs�ra a 'Result Text' mez�ben megjelennek az egyes tal�latok.  

A keres�s megfuttat�sa ut�n elt�nik a 'Start' gomb �s megjelenik egy 'Info panel' gomb:

![Keres�s](../../Doc/MainWindowMatch.png)  

Az 'Info panel' gomb seg�ts�g�vel tov�bbi inform�ci�t k�rhet�nk le a keres�sr�l. Az 'Info panel' mindaddig l�that� marad, am�g a keres�si felt�telek nem v�ltoznak. 
A keres�si felt�telek v�ltoz�s�ra az 'Info panel' gomb elt�nik �s ism�t megjelenik a 'Start' gomb.

Az inform�ci� panelt a jobb fels� sarokban l�v� k�k k�rlap gombbal z�rhatjuk be. Am�g az inform�ci�s panel nyitva van, addig a f� panel inakt�v.

#### Eredm�ny r�szletez�se

A 'Result detail' f�l�n megn�zhetj�k az egyes tal�latokhoz a k�vetkez� adatokat:  
- csoport n�v,  
- tal�lat eredm�nye, vagyis az a karakterl�nc, ami megfelelt a keres�si mint�nak,  
- tal�lat indexe, vagyis az a karakter index, ahonnan a tal�lat karakterl�nc kezd�dik az eredeti sz�vegben �s  
- a tal�lt karakterl�nc hossza.  

![Eredm�ny r�szletez�se](../../Doc/ResultInfo.png)  

#### Eredm�ny r�szletez�se t�bl�zattal

A 'Result table' f�l�n ugyanazokat az adatokat lehet megn�zni itt is, mint az el�bbi pontban le�rtak, csak t�bl�zatba foglalva.

![Eredm�ny r�szletez�se t�bl�zattal](../../Doc/ResultTable.png)  

#### Eredm�ny grafikon

A 'Result graph' f�l�n grafikus form�ban is megtekinthet�, hogy melyik indexen mikor milyen tal�lati esem�ny t�rt�nt.

![Eredm�ny r�szletez�se t�bl�zattal](../../Doc/ResultGraph.png) 

### Keres�s

A leny�l� list�ban a 'Replace' kiv�laszt�s�val haszn�lhat� a csere funkci�. A csere hasonl� lehet�s�geket biztos�t, mint a keres�s, azzal a 2 k�l�nbs�ggel, hogy a 'Result Text' mez�ben
a csere eredm�nye jelenik meg, valamint megjelenik egy 'Replacement' mez�, ahova a csere mint�j�t lehet be�rni.
