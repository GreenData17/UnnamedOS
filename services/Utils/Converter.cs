using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnnamedOS.services.Utils
{
    public class Converter
    {
        public static string HexToAscii(byte hex)
        {
            switch (hex)
            {
                // control character

                case 0x00:
                    return "NUL";
                case 0x03:
                    return "ETX"; // end of text
                case 0x07:
                    return "BEL"; // BEEP sound
                case 0x08:
                    return "BS";  // Backspace
                case 0x09:
                    return "HT";  // Horizontal Tab
                case 0x1B:
                    return "ESC"; // Escape
                case 0x1C:
                    return "FS";  // File separator

                // basic characters

                case 0x20:
                    return " ";
                case 0x21:
                    return "!";
                case 0x22:
                    return "\"";
                case 0x23:
                    return "#";
                case 0x24:
                    return "$";
                case 0x25:
                    return "%";
                case 0x26:
                    return "&";
                case 0x27:
                    return "'";
                case 0x28:
                    return "(";
                case 0x29:
                    return ")";
                case 0x2A:
                    return "*";
                case 0x2B:
                    return "+";
                case 0x2C:
                    return ",";
                case 0x2D:
                    return "-";
                case 0x2E:
                    return ".";
                case 0x2F:
                    return "/";
                case 0x30:
                    return "0";
                case 0x31:
                    return "1";
                case 0x32:
                    return "2";
                case 0x33:
                    return "3";
                case 0x34:
                    return "4";
                case 0x35:
                    return "5";
                case 0x36:
                    return "6";
                case 0x37:
                    return "7";
                case 0x38:
                    return "8";
                case 0x39:
                    return "9";
                case 0x3A:
                    return ":";
                case 0x3B:
                    return ";";
                case 0x3C:
                    return "<";
                case 0x3D:
                    return "=";
                case 0x3E:
                    return ">";
                case 0x3F:
                    return "?";
                case 0x40:
                    return "@";
                case 0x41:
                    return "A";
                case 0x42:
                    return "B";
                case 0x43:
                    return "C";
                case 0x44:
                    return "D";
                case 0x45:
                    return "E";
                case 0x46:
                    return "F";
                case 0x47:
                    return "G";
                case 0x48:
                    return "H";
                case 0x49:
                    return "I";
                case 0x4A:
                    return "J";
                case 0x4B:
                    return "K";
                case 0x4C:
                    return "L";
                case 0x4D:
                    return "M";
                case 0x4E:
                    return "N";
                case 0x4F:
                    return "O";
                case 0x50:
                    return "P";
                case 0x51:
                    return "Q";
                case 0x52:
                    return "R";
                case 0x53:
                    return "S";
                case 0x54:
                    return "T";
                case 0x55:
                    return "U";
                case 0x56:
                    return "V";
                case 0x57:
                    return "W";
                case 0x58:
                    return "X";
                case 0x59:
                    return "Y";
                case 0x5A:
                    return "Z";
                case 0x5B:
                    return "[";
                case 0x5C:
                    return "\\";
                case 0x5D:
                    return "]";
                case 0x5E:
                    return "^";
                case 0x5F:
                    return "_";
                case 0x60:
                    return "`";
                case 0x61:
                    return "a";
                case 0x62:
                    return "b";
                case 0x63:
                    return "c";
                case 0x64:
                    return "d";
                case 0x65:
                    return "e";
                case 0x66:
                    return "f";
                case 0x67:
                    return "g";
                case 0x68:
                    return "h";
                case 0x69:
                    return "i";
                case 0x6A:
                    return "j";
                case 0x6B:
                    return "k";
                case 0x6C:
                    return "l";
                case 0x6D:
                    return "m";
                case 0x6E:
                    return "n";
                case 0x6F:
                    return "o";
                case 0x70:
                    return "p";
                case 0x71:
                    return "q";
                case 0x72:
                    return "r";
                case 0x73:
                    return "s";
                case 0x74:
                    return "t";
                case 0x75:
                    return "u";
                case 0x76:
                    return "v";
                case 0x77:
                    return "w";
                case 0x78:
                    return "x";
                case 0x79:
                    return "y";
                case 0x7A:
                    return "z";
                case 0x7B:
                    return "{";
                case 0x7C:
                    return "|";
                case 0x7D:
                    return "}";
                case 0x7E:
                    return "~";
                case 0x7F:
                    return "DEL";

                // Extended ASCII
                // Only CP437 supported (its a ascii character table name)

                case 0x80:
                    return "Ç";
                case 0x81:
                    return "ü";
                case 0x82:
                    return "é";
                case 0x83:
                    return "â";
                case 0x84:
                    return "ä";
                case 0x85:
                    return "à";
                case 0x86:
                    return "å";
                case 0x87:
                    return "ç";
                case 0x88:
                    return "ê";
                case 0x89:
                    return "ë";
                case 0x8A:
                    return "è";
                case 0x8B:
                    return "ï";
                case 0x8C:
                    return "î";
                case 0x8D:
                    return "ì";
                case 0x8E:
                    return "Ä";
                case 0x8F:
                    return "Å";
                case 0x90:
                    return "É";
                case 0x91:
                    return "æ";
                case 0x92:
                    return "Æ";
                case 0x93:
                    return "ô";
                case 0x94:
                    return "ö";
                case 0x95:
                    return "ò";
                case 0x96:
                    return "û";
                case 0x97:
                    return "ù";
                case 0x98:
                    return "ÿ";
                case 0x99:
                    return "Ö";
                case 0x9A:
                    return "Ü";
                case 0x9B:
                    return "¢";
                case 0x9C:
                    return "£";
                case 0x9D:
                    return "¥";
                case 0x9E:
                    return "₧";
                case 0x9F:
                    return "ƒ";
                case 0xA0:
                    return "á";
                case 0xA1:
                    return "í";
                case 0xA2:
                    return "ó";
                case 0xA3:
                    return "ú";
                case 0xA4:
                    return "ñ";
                case 0xA5:
                    return "Ñ";
                case 0xA6:
                    return "ª";
                case 0xA7:
                    return "º";
                case 0xA8:
                    return "¿";
                case 0xA9:
                    return "⌐";
                case 0xAA:
                    return "¬";
                case 0xAB:
                    return "½";
                case 0xAC:
                    return "¼";
                case 0xAD:
                    return "¡";
                case 0xAE:
                    return "«";
                case 0xAF:
                    return "»";
                case 0xB0:
                    return "░";
                case 0xB1:
                    return "▒";
                case 0xB2:
                    return "▓";
                case 0xB3:
                    return "│";
                case 0xB4:
                    return "┤";
                case 0xB5:
                    return "╡";
                case 0xB6:
                    return "╢";
                case 0xB7:
                    return "╖";
                case 0xB8:
                    return "╕";
                case 0xB9:
                    return "╣";
                case 0xBA:
                    return "║";
                case 0xBB:
                    return "╗";
                case 0xBC:
                    return "╝";
                case 0xBD:
                    return "╜";
                case 0xBE:
                    return "╛";
                case 0xBF:
                    return "┐";
                case 0xC0:
                    return "└";
                case 0xC1:
                    return "┴";
                case 0xC2:
                    return "┬";
                case 0xC3:
                    return "├";
                case 0xC4:
                    return "─";
                case 0xC5:
                    return "┼";
                case 0xC6:
                    return "╞";
                case 0xC7:
                    return "╟";
                case 0xC8:
                    return "╚";
                case 0xC9:
                    return "╔";
                case 0xCA:
                    return "╩";
                case 0xCB:
                    return "╦";
                case 0xCC:
                    return "╠";
                case 0xCD:
                    return "═";
                case 0xCE:
                    return "╬";
                case 0xCF:
                    return "╧";
                case 0xD0:
                    return "╨";
                case 0xD1:
                    return "╤";
                case 0xD2:
                    return "╥";
                case 0xD3:
                    return "╙";
                case 0xD4:
                    return "╘";
                case 0xD5:
                    return "╒";
                case 0xD6:
                    return "╓";
                case 0xD7:
                    return "╫";
                case 0xD8:
                    return "╪";
                case 0xD9:
                    return "┘";
                case 0xDA:
                    return "┌";
                case 0xDB:
                    return "█";
                case 0xDC:
                    return "▄";
                case 0xDD:
                    return "▌";
                case 0xDE:
                    return "▐";
                case 0xDF:
                    return "▀";
                case 0xE0:
                    return "α";
                case 0xE1:
                    return "ß";
                case 0xE2:
                    return "Γ";
                case 0xE3:
                    return "π";
                case 0xE4:
                    return "Σ";
                case 0xE5:
                    return "σ";
                case 0xE6:
                    return "µ";
                case 0xE7:
                    return "τ";
                case 0xE8:
                    return "Φ";
                case 0xE9:
                    return "Θ";
                case 0xEA:
                    return "Ω";
                case 0xEB:
                    return "δ";
                case 0xEC:
                    return "∞";
                case 0xED:
                    return "φ";
                case 0xEE:
                    return "ε";
                case 0xEF:
                    return "∩";
                case 0xF0:
                    return "≡";
                case 0xF1:
                    return "±";
                case 0xF2:
                    return "≥";
                case 0xF3:
                    return "≤";
                case 0xF4:
                    return "⌠";
                case 0xF5:
                    return "⌡";
                case 0xF6:
                    return "÷";
                case 0xF7:
                    return "≈";
                case 0xF8:
                    return "°";
                case 0xF9:
                    return "∙";
                case 0xFA:
                    return "·";
                case 0xFB:
                    return "√";
                case 0xFC:
                    return "ⁿ";
                case 0xFD:
                    return "²";
                case 0xFE:
                    return "■";
                case 0xFF:
                    return "";



                default:
                    return "?";
            }
        }

        public static string ByteToHex(byte b) => b.ToString("X2");

        public static string[] ByteArrayToHexArray(byte[] b)
        {
            List<string> s = new List<string>();
            foreach (var b1 in b)
            {
                s.Add(ByteToHex(b1));
            }

            return s.ToArray();
        }

        public static byte HexToByte(string hex)
        {
            int index = hex.Length - 1;
            int result = 0;

            foreach (var c in hex)
            {
                if (c == 'A') result += ToPower(10, index);
                else if (c == 'B') result += ToPower(11, index);
                else if (c == 'C') result += ToPower(12, index);
                else if (c == 'D') result += ToPower(13, index);
                else if (c == 'E') result += ToPower(14, index);
                else if (c == 'F') result += ToPower(15, index);
                else if (c == '0') { index -= 1; continue; }
                else result += ToPower(int.Parse(c.ToString()), index);

                index -= 1;
            }

            return (byte)result;
        }

        public static string HexToDecimal(string hex)
        {
            int index = hex.Length - 1;
            int result = 0;

            foreach (var c in hex)
            {

                if (c == 'A') result += ToPower(10, index);
                else if (c == 'B') result += ToPower(11, index);
                else if (c == 'C') result += ToPower(12, index);
                else if (c == 'D') result += ToPower(13, index);
                else if (c == 'E') result += ToPower(14, index);
                else if (c == 'F') result += ToPower(15, index);
                else if (c == '0') { index -= 1; continue; }
                else result += ToPower(int.Parse(c.ToString()), index);

                index -= 1;
            }

            return result.ToString();
        }

        private static int ToPower(int number, int power)
        {
            int result = 1;

            if (power == 0) { return 15;}

            for (int i = 0; i < power; i++)
            {
                result *= 16;
            }

            return result * number;
        }

        public static string HexToUnsignedDecimal(string hex)
        {
            int index = hex.Length - 1;
            uint result = 0;

            foreach (var c in hex)
            {

                if (c == 'A') result += UnsignedToPower(10, index);
                else if (c == 'B') result += UnsignedToPower(11, index);
                else if (c == 'C') result += UnsignedToPower(12, index);
                else if (c == 'D') result += UnsignedToPower(13, index);
                else if (c == 'E') result += UnsignedToPower(14, index);
                else if (c == 'F') result += UnsignedToPower(15, index);
                else if (c == '0') { index -= 1; continue; }
                else result += UnsignedToPower(int.Parse(c.ToString()), index);

                index -= 1;
            }

            return result.ToString();
        }

        private static uint UnsignedToPower(int number, int power)
        {
            uint result = 1;

            if (power == 0) { return 15; }

            for (int i = 0; i < power; i++)
            {
                result *= 16;
            }

            return (uint)(result * number);
        }
    }
}
