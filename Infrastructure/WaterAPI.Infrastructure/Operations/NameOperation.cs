using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Infrastructure.StaticService
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        // --- Türkçe Karakterlerin İngilizce Karşılıklarına Dönüştürülmesi ---
        => name.Replace("ç", "c")
        .Replace("Ç", "C")
        .Replace("ğ", "g")
        .Replace("Ğ", "G")
        .Replace("ı", "i")
        .Replace("İ", "I")
        .Replace("ö", "o")
        .Replace("Ö", "O")
        .Replace("ş", "s")
        .Replace("Ş", "S")
        .Replace("ü", "u")
        .Replace("Ü", "U")

        // --- Kabul Edilmeyen İşaretlerin ve Boşluğun Silinmesi ---
        .Replace(" ", "")      // Boşluk
        .Replace("!", "")      // Ünlem
        .Replace("'", "")      // Tek Tırnak
        .Replace("\"", "")     // Çift Tırnak
        .Replace("^", "")      // Şapka
        .Replace("+", "")      // Artı
        .Replace("%", "")      // Yüzde
        .Replace("&", "")      // Ve İşareti
        .Replace("/", "")      // Bölü
        .Replace("(", "")      // Parantez Aç
        .Replace(")", "")      // Parantez Kapat
        .Replace("=", "")      // Eşittir
        .Replace("?", "")      // Soru İşareti
        .Replace("_", "")      // Alt Tire
        .Replace("-", "")      // Kısa Çizgi
        .Replace("*", "")      // Yıldız
        .Replace(".", "")      // Nokta
        .Replace(":", "")      // İki Nokta
        .Replace(";", "")      // Noktalı Virgül
        .Replace(",", "")      // Virgül
        .Replace("|", "")      // Pipe
        .Replace("<", "")      // Küçüktür
        .Replace(">", "")      // Büyüktür
        .Replace("@", "")      // @ İşareti
        .Replace("$", "")      // Dolar
        .Replace("€", "")      // Euro
        .Replace("£", "")      // Sterlin
        .Replace("`", "")      // Aksan
        .Replace("~", "")      // Tilde
        .Replace("{", "")      // Süslü Parantez Aç
        .Replace("}", "")      // Süslü Parantez Kapat
        .Replace("[", "")      // Köşeli Parantez Aç
        .Replace("]", "")      // Köşeli Parantez Kapat
        .Replace("\\", "");    // Ters Taksim

    }
}
