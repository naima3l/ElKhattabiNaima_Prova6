using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElKhattabiNaima_Prova6.Core.Models;
using ElKhattabiNaima_Prova6.EF.Repositories;

namespace ElKhattabiNaima_Prova6.Client
{
    public class Menu
    {
        private static MainBL mainBL = new MainBL(new EFUserRepository(), new EFPolicyRepository());

        internal static void Start()
        {
            int choice;
            bool check = true;
            do
            {
                Console.WriteLine("Benvenuto!");

                Console.WriteLine("Premi 1 per inserire un nuovo cliente \nPremi 2 per inserire una polizza per un cliente esistente \nPremi 3 visualizzare le polizze di un cliente \nPremi 4 per posticipare la data di scadenza \nPremi 5 per visualizzare i clienti che hanno una polizza vita \nPremi 0 per uscire");

                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 5)
                {
                    Console.WriteLine("Scelta non valida! Riprova.");
                }

                switch (choice)
                {
                    case 1:
                        //aggiungi utente
                        AddNewUser();
                        break;
                    case 2:
                        //inserire una polizza per un cliente esistente
                        AddPolicyToExistingUser();
                        break;
                    case 3:
                        //visualizzare le polizze di un cliente
                        ShowUserPolicies();
                        break;
                    case 4:
                        //posticipare la data di scadenza
                        PosticipateExpirationDate();
                        break;
                    case 5:
                        // visualizzare i clienti che hanno una polizza vita
                        ShowUsersByPolicy();
                        break;
                    case 0:
                        check = false;
                        return;
                }
            } while (check);
        }

        private static void AddNewUser()
        {
            string cf;
            string name, lastname;
            //Policy policy = new Policy();
            bool check = true;
            do
            {
                Console.WriteLine("Inserisci il CF");
                cf = Console.ReadLine();
            } while (cf.Length == 0 || cf.Length < 16 || cf.Length > 16);

            check = mainBL.GetByCF(cf);
            if(check == true)
            {
                Console.WriteLine("Esiste già un cliente con questo codice fiscale.");
                return;
            }

            do
            {
                Console.WriteLine("Inserisci il Nome");
                name = Console.ReadLine();
            } while (name.Length == 0);

            do
            {
                Console.WriteLine("Inserisci il cognome");
                lastname = Console.ReadLine();
            } while (lastname.Length == 0);

            User user = new User
            {
                CF = cf,
                Name = name,
                LastName = lastname
                //,
                //policies = policies
            };
            bool done = mainBL.AddUser(user);
            if (done == true)
            {
                Console.WriteLine("Il cliente è stato aggiunto con successo");
            }
            else Console.WriteLine("Si è verificato un problema nell'aggiunta del cliente");
        }

        private static Policy GetPolicyByNumber(int num)
        {
            var policy = mainBL.GetPolicyByNumber(num);
            return policy;
        }

        private static void AddPolicyToExistingUser()
        {
            string cf;
            bool check, c;
            Policy policy = new Policy();
            User user = new User();
            int number;
            DateTime date;
            decimal payment;
            do
            {
                Console.WriteLine("Inserisci il CF");
                cf = Console.ReadLine();
            } while (cf.Length == 0 || cf.Length < 16 || cf.Length > 16);

            check = mainBL.GetByCF(cf);
            if (check != true)
            {
                Console.WriteLine("Non esiste nessun cliente con questo codice fiscale.");
                return;
            }
            else
            {
                user = mainBL.GetUserByCF(cf);
                do
                {
                    Console.WriteLine("Scegli il tipo di polizza");
                    Console.WriteLine($"Premi 1 per {EnumPolicyType.RCAuto}, 2 per {EnumPolicyType.Furto}, 3 per {EnumPolicyType.Vita}");
                    int num;
                    while (!int.TryParse(Console.ReadLine(), out num) || num < 0 || num > 3)
                    {
                        Console.WriteLine("Tipo polizza inesistente. Riprova");
                    }
                    switch (num)
                    {
                        case 1:
                            policy.Type = EnumPolicyType.RCAuto;
                            break;
                        case 2:
                            policy.Type = EnumPolicyType.Furto;
                            break;
                        case 3:
                            policy.Type = EnumPolicyType.Vita;
                            break;
                    }
                } while (policy.Type == null);

                
                do
                {
                    Console.WriteLine("Inserisci il numero della nuova polizza");
                    while (!int.TryParse(Console.ReadLine(), out number) || number <= 0 )
                    {
                        Console.WriteLine("Riprova");
                    }
                    bool n = CheckPolicyNumber(number);
                    if(n == true)
                    {
                        Console.WriteLine("Esiste già una polizza con questo numero");
                        return;
                    }
                    c = false;
                } while (c);
                policy.PolicyNumber = number;

                do
                {
                    Console.WriteLine("Inserisci la data di scadenza");
                    while (!DateTime.TryParse(Console.ReadLine(), out date) || date <= DateTime.Now)
                    {
                        Console.WriteLine("Inserisci una data valida e posteriore ad oggi");
                    }
                } while (date == null);
                policy.ExpirationDate = date;

                do
                {
                    Console.WriteLine("Inserisci il pagamento mensile");
                    while (!decimal.TryParse(Console.ReadLine(), out payment) || payment <= 0)
                    {
                        Console.WriteLine("Inserisci un prezzo valido");
                    }
                    c = false;
                } while (c);
                policy.MontlyPayment = payment;
                policy.User = user;
            }

            
            c = mainBL.AddPolicyToExistingUser(policy);
            if (c == true)
            {
                Console.WriteLine("La polizza è stata inserita correttamente");
            }
            else Console.WriteLine("Si è verificato un problema nell'inserimento della polizza");
        }

        private static bool CheckPolicyNumber(int num)
        {
            return mainBL.CheckPolicyNumber(num);
        }

        private static void ShowUserPolicies()
        {
            string cf;
            bool check;
            User user = new User();
            do
            {
                Console.WriteLine("Inserisci il CF del cliente di cui vuoi visualizzare le policies");
                cf = Console.ReadLine();
            } while (cf.Length == 0 || cf.Length < 16 || cf.Length > 16);

            check = mainBL.GetByCF(cf);
            if (check != true)
            {
                Console.WriteLine("Non esiste nessun cliente con questo codice fiscale.");
                return;
            }
            else
            {
                user = mainBL.GetUserByCF(cf);
                List<Policy> policies = mainBL.FetchUserPolicies(user);
                if(policies.Count == 0)
                {
                    Console.WriteLine($"Il cliente {user.Name} non ha nessuna polizza disponibile al momento");
                    return;
                }
                else
                {
                    Console.WriteLine($"Le polizze del cliente {user.Name} {user.LastName} sono : ");
                    int i = 1;
                    foreach (var p in policies)
                    {
                        Console.WriteLine($"{i}. Numero : {p.PolicyNumber}, Tipo : {p.Type}, Data di scadenza : {p.ExpirationDate}, Pagamento mensile : {p.MontlyPayment} ");
                        i++;
                    }
                }
            }
        }

        private static void PosticipateExpirationDate()
        {
            ShowPolicies();
            int number;
            bool c = true;
            Policy policy = new Policy();
            DateTime date;

            do
            {
                Console.WriteLine("Inserisci il numero polizza di cui vuoi modificare la data di scadenza");
                while (!int.TryParse(Console.ReadLine(), out number) || number <= 0)
                {
                    Console.WriteLine("Riprova");
                }
                bool n = CheckPolicyNumber(number);
                if (n != true)
                {
                    Console.WriteLine("Non esiste una polizza con questo numero");
                    return;
                }
                c = false;
            } while (c);
            policy = GetPolicyByNumber(number);

            do
            {
                Console.WriteLine("Inserisci la nuova data di scadenza");
                while (!DateTime.TryParse(Console.ReadLine(), out date) || date <= DateTime.Now || date <= policy.ExpirationDate)
                {
                    Console.WriteLine("Inserisci una data valida e posteriore a quella di oggi e a quella già inserita nella polizza");
                }
            } while (date == null);
            policy.ExpirationDate = date;

            c = mainBL.PosticipateExpirationDate_Disconnected(policy);
            if (c == false)
            {
                Console.WriteLine("Si è verificato un problema nella modifica della data della polizza");
            }
            else Console.WriteLine("La data di scadenza della polizza è stata aggiornata correttamente");
        }

        private static void ShowPolicies()
        {
            List<Policy> policies = mainBL.FetchPolicies();
            if( policies.Count == 0)
            {
                Console.WriteLine("Non ci sono polizze disponibili");
            }
            else
            {
                foreach (var p in policies)
                {
                    Console.WriteLine($" Numero : {p.PolicyNumber}, Tipo : {p.Type}, Data di scadenza : {p.ExpirationDate}, Pagamento mensile : {p.MontlyPayment} ");
                }
            }
        }

        private static void ShowUsersByPolicy()
        {
            List<User> users = mainBL.FetchUsersByPolicy();
            if (users.Count == 0)
            {
                Console.WriteLine("Non ci sono clienti che hanno attivato una polizza VITA disponibili");
            }
            else
            {
                Console.WriteLine("I clienti che hanno attivato una polizza vita sono : ");
                int i = 1;
                foreach (var u in users)
                {
                    Console.WriteLine($"{i}. Nome : {u.Name}, Cognome : {u.LastName}");
                    i++;
                }
            }
        }
    }
}
