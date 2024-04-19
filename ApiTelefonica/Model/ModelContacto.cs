namespace ApiTelefonica.Model
{
    public class ModelContacto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cumpleano { get; set; }
        public Dictionary<string, int> Numeros { get; set; }
        public Dictionary<string, string> Emails { get; set; }
    }

    static class ContactoRepository
    {
        private static List<ModelContacto> contacts = new List<ModelContacto>();

        public static void AddContact(ModelContacto contact)
        {
            contacts.Add(contact);
        }

        public static List<ModelContacto> GetAllContacts()
        {
            return contacts;
        }

        public static void DeleteContact(ModelContacto contact)
        {
            contacts.Remove(contact);
        }
    }

}
