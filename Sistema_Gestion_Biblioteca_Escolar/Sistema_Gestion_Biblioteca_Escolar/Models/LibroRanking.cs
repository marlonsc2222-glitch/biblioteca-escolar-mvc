namespace Sistema_Gestion_Biblioteca_Escolar.Models
{
    public class LibroRanking // Fila del ranking: no es una tabla, solo transporta el resultado de la consulta
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int VecesPrestado { get; set; } // Cantidad total de préstamos (incluye los ya devueltos)
    }
}