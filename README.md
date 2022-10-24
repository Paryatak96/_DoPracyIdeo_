# _DoPracyIdeo_

!!! - Należy mieć zainstalowany program Visual Studio 2019 lub nowszy

!!! - Należy mieć zainstalowany MSSQL Server management Studio




Intrukcja do zainstalowania MSSQL Server Management Studio:

1. Klikamy w link obok: https://www.microsoft.com/pl-pl/sql-server/sql-server-downloads

2. Schodzimy do nagłówka "Lub pobierz bezpłatną, specjalistyczną edycję"

3. Wybiermy opcję "Express" i klikamy "Pobierz teraz" 

4. Po pobraniu klikamy w pobrany element (instalator).

5. Po uruchomieniu się instalatora wybieramy opcję "Basic", akceptujemy wszystkie zgody i czekamy na zainstalowanie się programu.

6. Po instalacji MUSIMY skopiować linijkę pod nazwą "CONNECTION STRING" i najlepiej wkleić do pliku tekstowego i zapisać (będziemy tego jeszcze używać).

7. Koniec.


Instrukcja do uruchomienia aplikacji:


1. Należy pobrać folder z aplikacją, aby to zrobić należy:

   - Kliknąć w link obok: https://github.com/Paryatak96/_DoPracyIdeo_
   - Kliknąć zielony przycisk o nazwie "Code"
   - Wybrać opcję "Download ZIP"
   
2. Kiedy mamy pobrany folder, należy go wypakować w wybranym przez Państwa miejscu.

3. Po wypakowaniu pliku należy otworzyć plik o nazwie "TreeManagementFolder.sln"

4. Po otwarciu w programie Visual Studio należy przejść do pliku o nazwie "appsettings.json"

5. W linijce 3 zatytułowanej jako "DefaultConnection" po dwukropku należy wkleić wcześniej skopiowaną linijkę "CONNECTION STRING" w cudzysłów.

6.

7. Po wklejeniu linijki "CONNECTION STRING" należy w programie Visual Studio na samej górze wybrać zakładkę "Tools" zjechać do "NuGet Package Manager" i wybrać "Package Manager Console"

8. Domyślnie na samym powinna otworzyć się konsola "Package Manager Console" 

9. Pod nazwą konsoli mamy linię "Package Source" która powinna być ustawiona na "All" oraz "Default Project" który musimy ustawić na "TreeManagementFolderMVC.Infrastructure"

10. Wpisujemy komendę: add-migration Initial

11. Po otrzymaniu informacji "Build succeeded" należy wpisać kolejną komendę: update-database

12. Po otrzymaniu informacji "Done" można uruchomić program klikając przycisk "IIS Express" :)


 
