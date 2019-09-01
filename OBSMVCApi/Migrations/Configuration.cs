namespace OBSMVCApi.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OBSMVCApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OBSMVCApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!context.Authors.Any())
            {
                var author = new List<Author>
                {
                    new Author{AuthorName = "Zaheer Raihan",DoB =DateTime.Parse("08/19/1935"),ContactNo = "01911",Email = "zahir@mail.com",Address = "Feni Mahakuma, Bengal Presidency, British India",AuthorInfo = "Zahir Raihan (19 August 1935 - disappeared 30 January 1972) was a Bangladeshi novelist, writer and filmmaker. He is the most notable for his documentary Stop Genocide (1971), made during the Bangladesh Liberation War. He was posthumously awarded Ekushey Padak in 1977 and Independence Day Award in 1992 by the Government of Bangladesh.",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "Muhammad Zafar Iqbal",DoB =DateTime.Parse("12/23/1952"),ContactNo = "01611",Email = "zafo9r@mail.com",Address = "Sylhet",AuthorInfo = "Professor of Computer Science and Engineering at Shahjalal University of Science and Technology (SUST)",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "Anisul Hoque",DoB =DateTime.Parse("03/04/1965"),ContactNo = "01915396311",Email = "anisul@mail.com",Address = "Rangpur",AuthorInfo = "Anisul Hoque was born in Rangpur in 1965 to Mofazzal Hoque and Mrs Anwara Begum. He was the student of Rangpur PTI Primary School. He passed SSC examination from Rangpur Zilla School in 1981 and HSC examination from Rangpur Carmichael College in 1983. He graduated from Bangladesh University of Engineering and Technology (BUET), trained as a civil engineer.",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "Ayman Sadiq",DoB =DateTime.Parse("09/02/1992"),ContactNo = "01815306310",Email = "sadiq@mail.com",Address = "Comilla",AuthorInfo = "He was born on September 2. Ayman Sadiq is a Bangladeshi education entrepreneur and Internet personality. He founded www.10minuteschool.com in the 21st. It is such an institution; Which provides a variety of educational information and assistance online for free. The company has produced over two thousand videos for listeners (who are learning from here). This institute covers basic English, mathematics and science subjects covered by academic syllabus and how to increase proficiency on various subjects.",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "Arif Azad",DoB =DateTime.Parse("01/07/1990"),ContactNo = "01786396320",Email = "arif@mail.com",Address = "Chattogram",AuthorInfo = "Arif Azad is a Bangladeshi writer and activist who wrote more than two books. Both of his books became bestseller in 'Ekushe Boimela', a Bangladeshi yearly national book fair. His first book is Paradoxical Sajid. The book has been translated into English and Assamese language as well. And his second book is Aroj Ali Somipe. He also led protests in 2016 against the banning of Peace TV in Bangladesh. He is the founder of a charity organization named 'As-Sadik Foundation'.",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "Paulo Coelho",DoB =DateTime.Parse("08/24/1947"),ContactNo = "01851398920",Email = "coelho@mail.com",Address = "Chattogram",AuthorInfo = "Coelho was born in Rio de Janeiro, Brazil and attended a Jesuit school. As a teenager, Coelho wanted to become a writer. Upon telling his mother this, she responded, My dear, your father is an engineer. He is a logical, reasonable man with a very clear vision of the world. Do you actually know what it means to be a writer? At 17, Coelho's introversion and opposition led to a traditional path following his parents committing him to a mental institution from which he escaped three times.",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "J. K. Rowling",DoB =DateTime.Parse("06/30/1965"),ContactNo = "01715398955",Email = "rowling@mail.com",Address = "Sylhet",AuthorInfo = "Joanne Rowling CH, OBE, HonFRSE, FRCPE, FRSL (rolling; born 31 July 1965), better known by her pen name is J. K. Rowling, a British novelist, screenwriter, producer, and philanthropist. She is best known for writing the Harry Potter fantasy series, which has won multiple awards and sold more than 500 million copies, becoming the best-selling book series in history. The Harry Potter books have also been based on the popular film series of the same name, over which Row.",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "Avul Pakir Jainulabdeen Abdul Kalam",DoB =DateTime.Parse("09/15/1931"),ContactNo = "01725639544",Email = "pakir@mail.com",Address = "Muradpur",AuthorInfo = "Avul Pakir Jainulabdeen Abdul Kalam (About this soundlisten); 15 October 1931 - 27 July 2015) was an aerospace scientist who served as the 11th President of India from 2002 to 2007. He was born and raised in Rameswaram, Tamil. Nadu and studied physics and aerospace engineering. He spent the next four decades as a scientist and science administrator, at the Defense Research and Development Organization (DRDO) and the Indian Space Research Organization (ISRO) and was intimately.",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "Napoleon Hill",DoB =DateTime.Parse("10/26/1883"),ContactNo = "01815398025",Email = "napoleon@mail.com",Address = "Hathazari",AuthorInfo = "Oliver Napoleon Hill (born October 26, 1883 - November 8, 1970) was an American self-help author. He is best known for his book Think and Grow Rich (1937), which is among the 10 best-selling self-help books of all time. Hill's work insists that fervid expectations are essential to improving one's life. Most of his books were promoted as expounding principles to achieve success.",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "James Allen",DoB =DateTime.Parse("01/26/1912"),ContactNo = "01520983620",Email = "james@mail.com",Address = "Comilla",AuthorInfo = "James Allen (28 November 1864 - 24 January 1912) was a British philosophical writer known for his inspirational books and poetry and as a pioneer of the self-help movement. His best-known work, as a Man Thinketh, has been mass-produced since its publication in 1903. It has been a source of inspiration to motivational and self-help authors.",ImageUrl = "none",IsActive = true},
                    new Author{AuthorName = "Peter B. Kyne",DoB =DateTime.Parse("10/12/1880"),ContactNo = "01913983694",Email = "kyne@mail.com",Address = "Chattogram",AuthorInfo = "Peter B. Kyne (October 12, 1880 - November 25, 1957) was an American novelist who published between 1904 and 1940. He was born and died in San Francisco, California. Many of his works were adapted into screenplays during the silent film era, especially his first novel, The Three Godfathers, which was published in 1913 and proved to be a huge success. More than 100 films were adapted from his works between 1914 and 1952, many without the earliest consent or compensation.",ImageUrl = "none",IsActive = true}
                };
                author.ForEach(a => context.Authors.AddOrUpdate(a));
                context.SaveChanges();
            }
            if (!context.Categories.Any())
            {
                var category = new List<Category>
                {
                    new Category{CategoryName = "Nobel",IsActive = true},
                    new Category{CategoryName = "Golpo Somogroo",IsActive = true},
                    new Category{CategoryName = "Shisu-Kisor Golpoo",IsActive = true},
                    new Category{CategoryName = "Motivational and Meditation",IsActive = true},
                    new Category{CategoryName = "Islamic stories",IsActive = true},
                    new Category{CategoryName = "Fiction",IsActive = true},
                    new Category{CategoryName = "Childrens Fiction Books",IsActive = true},
                    new Category{CategoryName = "Non-Fiction",IsActive = true},
                    new Category{CategoryName = "Adventure",IsActive = true},
                    new Category{CategoryName = "Mystery",IsActive = true},
                    new Category{CategoryName = "Mechanical Engineering",IsActive = true}
                };

                category.ForEach(c => context.Categories.AddOrUpdate(c));
                context.SaveChanges();
            }
            if (!context.Publishers.Any())
            {
                var publisher = new List<Publisher>
                {
                    new Publisher{PublisherName = "Onupom Prokashoni",ContactNo = "01918963522",Email = "onu@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Onnona",ContactNo = "01714582140",Email ="onnona@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "Somoy Prokasoni",ContactNo = "01915150360",Email = "somoy@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "Adorsho",ContactNo = "01859852456",Email = "adorsho@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Oddowon",ContactNo = "01986321475",Email = "synch@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "Synchronous Publishing",ContactNo = "01915468213",Email = "harppu@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Harpercollins publishers",ContactNo = "01846123546",Email = "bloopub@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Bloomsbury Publishing",ContactNo = "01715612350",Email = "unipress@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Universities Press",ContactNo = "01915456311",Email = "unipress@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Ballantine Books",ContactNo = "01563982546",Email = "ballan@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "Maanu Graphics Publishers",ContactNo = "01456932595",Email = "maanu@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "IBD Publishing",ContactNo = "01856402569",Email = "ibdpu@gmail.com",Address = "Gazipur",IsActive = true},
                    new Publisher{PublisherName = "SIMON & SCHUSTER UK",ContactNo = "01856402545",Email = "simon@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "Harvill Secker",ContactNo = "01856402569",Email = "harvill@gmail.com",Address = "Gazipur",IsActive = true},
                    new Publisher{PublisherName = "Allen Lane",ContactNo = "018564025645",Email = "allen@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "Penguin Books",ContactNo = "01856402528",Email = "Penguin@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Special Interest Model Books",ContactNo = "01856402569",Email = "specialbook@gmail.com",Address = "Gazipur",IsActive = true},
                    new Publisher{PublisherName = "Penguin Random House",ContactNo = "0185641236",Email = "random@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Corgi Books",ContactNo = "01756402569",Email = "corgi@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "Vintage Books",ContactNo = "01745693256",Email = "vintage@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "A Harper Collins Children Books",ContactNo = "01916402560",Email = "harperc12@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Penguin Books Ltd",ContactNo = "01819302562",Email = "vintage@gmail.com",Address = "Dhaka",IsActive = true},
                    new Publisher{PublisherName = "Harpercollins publishers",ContactNo = "01856402556",Email = "harperc@gmail.com",Address = "Gazipur",IsActive = true},
                    new Publisher{PublisherName = "Puffin Books",ContactNo = "01766402561",Email = "puffin@gmail.com",Address = "Chattogram",IsActive = true},
                    new Publisher{PublisherName = "Butterfly",ContactNo = "01676402562",Email = "butterfly@gmail.com",Address = "Chattogram",IsActive = true},
                };

                publisher.ForEach(p => context.Publishers.AddOrUpdate(p));
                context.SaveChanges();
            }
            #region book
            //if (!context.Books.Any())
            //{
            //    var book = new List<Book>
            //    {
            //        new Book
            //        {
            //            BookName = "Sita",
            //            Descriptions =
            //                "Immerse yourself in book 2 of the Ram Chandra series, based on the Ramayana, the story of Lady Sita, written by the multi-million bestselling Indian Author Amish; the author who has transformed Indian Fiction with his unique combination of mystery, mythology, religious symbolism and philosophy. In this book, you will follow Lady Sita's journey from an Adopted Child to the Prime Minister to finding her true calling. You will find all the familiar characters you have heard of, like Lord Ram and Lord Lakshman and see more of Lord Hanuman and many others from Mithila",
            //            CategoryId = 1, AuthorId = 1, PublisherId = 1,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "1st Edition",
            //            ISBN = "123", TranslatorId = 1, NumberOfPage = 250, Language = "Bangla", ImageUrl = "no image",
            //            IsActive = true
            //        },
            //        new Book
            //        {
            //            BookName = "Think And Grow Rich",
            //            Descriptions =
            //                "Think And Grow Rich has earned itself the reputation of being considered a textbook for actionable techniques that can help one get better at doing anything, not just by rich and wealthy, but also by people doing wonderful work in their respective fields.There are hundreds and thousands of successful people in the world who can vouch for the contents of this book.At the time of author’s death, about 20 million copies had already been sold.",
            //            CategoryId = 2, AuthorId = 2, PublisherId = 2,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "", ISBN = "123",
            //            TranslatorId = 1, NumberOfPage = 230, Language = "Bangla", ImageUrl = "no image",
            //            IsActive = true
            //        },
            //        new Book
            //        {
            //            BookName = "The Complete Nobels Of Sherlock Holmes",
            //            Descriptions =
            //                "Whisked into the province of Keekatpur, which is under the fist of Lord Kali, Kalki sees the ignominy of death trumping life all around him.He learns that he has been born to cleanse the world he lives in, for which he must journey to the North and learn the ways of Lord Vishnu’s Avatar; from an immortal who wields an axe.",
            //            CategoryId = 3, AuthorId = 3, PublisherId = 3,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "2nd Edition",
            //            ISBN = "240", TranslatorId = 3, NumberOfPage = 670, Language = "Bangla", ImageUrl = "no image",
            //            IsActive = true
            //        },
            //        new Book
            //        {
            //            BookName = "Kalki",
            //            Descriptions =
            //                "Born in the quiet village of Shambala, Kalki Hari, son of Vishnuyath and Sumati, has no idea about his heritage until he is pitted against tragedies and battles.",
            //            CategoryId = 4, AuthorId = 4, PublisherId = 4,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "1st Edition",
            //            ISBN = "523", TranslatorId = 4, NumberOfPage = 150, Language = "Bangla", ImageUrl = "none",
            //            IsActive = true
            //        },
            //        new Book
            //        {
            //            BookName = "Narasima",
            //            Descriptions =
            //                "Narasimha, once a brave soldier, has left the war and lies low as a physician in a village.But a familiar face from his past seeks his help to stop the tyranny of the blind usurper Andhaka. If Narasimha refuses, the world might just end.What will he do And why did he leave the war in the first place Prahlad, the interim king of Kashyapuri, is torn between the ideals of his unrighteous father and his love for Lord Vishnu.",
            //            CategoryId = 5, AuthorId = 5, PublisherId = 5,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "2nd Edition",
            //            ISBN = "456", TranslatorId = 1, NumberOfPage = 456, Language = "Bangla", ImageUrl = "no image",
            //            IsActive = true
            //        },

            //        new Book
            //        {
            //            BookName = "The Last Avatar",
            //            Descriptions =
            //                "In the not-so-distant future, India has fallen, and the world is on the brink of an apocalyptic war.An attack by the terrorist group Invisible Hand has brutally eliminated the Indian Prime Minister and the union cabinet.As a national emergency is declared, chaos, destruction and terror reign supreme.",
            //            CategoryId = 6, AuthorId = 6, PublisherId = 6,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = " 1st Edition",
            //            ISBN = "163", TranslatorId = 2, NumberOfPage = 270, Language = "Bangla", ImageUrl = "no image",
            //            IsActive = true
            //        },

            //        new Book
            //        {
            //            BookName = "Kaalkoot",
            //            Descriptions =
            //                "KaalKoot brings together a forgotten Himalayan legend, an international conspiracy and nail-biting suspense in a gripping thriller.January 1944 - Holed up in a Himalayan hideout, freedom fighter Manohar Rai has to take a chilling decision – one that could mean life or death for millions of people. ",
            //            CategoryId = 3, AuthorId = 1, PublisherId = 2,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "2nd Edition",
            //            ISBN = "453", TranslatorId = 1, NumberOfPage = 560, Language = " Arabic", ImageUrl = "no image",
            //            IsActive = true
            //        },

            //        new Book
            //        {
            //            BookName = "42 Days Of Love ",
            //            Descriptions =
            //                "Donna lives for adventure, is a passionate explorer and loves deep sea diving.She has vowed to dig out the great sunken treasure of the Indian Nizam. Donna’s dark past and unhealed spiritual wounds drift her aboard an old submarine, along with a veteran handpicked crew. And as the sailors say, “Waves are not measured in feet or inches, they are measured in the increment of fear.” The expedition that ensues flips Donna’s life forever.",
            //            CategoryId = 2, AuthorId = 1, PublisherId = 3,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "1st Edition",
            //            ISBN = "5543", TranslatorId = 1, NumberOfPage = 230, Language = "Bangla", ImageUrl = "no image",
            //            IsActive = true
            //        },

            //        new Book
            //        {
            //            BookName = "The Secret Of The Palamu Fort",
            //            Descriptions =
            //                "When the Germans occupy Paris, father and daughter flee to Saint-Malo on the Brittany coast, where Marie-Laure’s agoraphobic great uncle lives in a tall, narrow house by the sea. ",
            //            CategoryId = 3, AuthorId = 3, PublisherId = 4,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "2nd Edition",
            //            ISBN = "963", TranslatorId = 2, NumberOfPage = 525, Language = "Bangla", ImageUrl = "none",
            //            IsActive = true
            //        },

            //        new Book
            //        {
            //            BookName = "All The Light We Cannot See",
            //            Descriptions =
            //                "When Marie Laure goes blind, aged six, her father builds her a model of their Paris neighbourhood, so she can memorize it with her fingers and then navigate the real streets.",
            //            CategoryId = 2, AuthorId = 3, PublisherId = 1,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "1st Edition",
            //            ISBN = "503", TranslatorId = 1, NumberOfPage = 260, Language = "Bangla", ImageUrl = " none",
            //            IsActive = true
            //        },

            //        new Book
            //        {
            //            BookName = "Siddhartha ",
            //            Descriptions =
            //                "Siddhartha is a novel by Hermann Hesse that deals with the spiritual journey of a boy known as Siddhartha from the Indian subcontinent during the time of Lord Buddha.",
            //            CategoryId = 1, AuthorId = 2, PublisherId = 3,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "1st Edition",
            //            ISBN = "719", TranslatorId = 4, NumberOfPage = 300, Language = "Bangla", ImageUrl = "none",
            //            IsActive = true
            //        },

            //        new Book
            //        {
            //            BookName = "The White Tiger",
            //            Descriptions =
            //                "Winner of the Man Booker Prize 2008 Meet Balram Halwai, the 'white tiger' servant, philosopher, entrepreneur, murderer… Born in a village in the dark heart of India, the son of a rickshaw puller, Balram is taken out of school and put to work in a teashop.As he crushes coal and wipes tables, he nurses a dream of escape. His big chance comes when a rich landlord hires him as a chauffeur for his son, daughter-in-law, and their two Pomeranian dogs.",
            //            CategoryId = 1, AuthorId = 2, PublisherId = 4,  CostPrice = 350.40M, SellingPrice=400.00M, Edition = "1st Edition",
            //            ISBN = "556983", TranslatorId = 2, NumberOfPage = 650, Language = "Bangla", ImageUrl = "none",
            //            IsActive = true
            //        },
            //        new Book
            //        {
            //            BookName = "Selection Day",
            //            Descriptions =
            //                "A page-turner of a novel set in the world of cricket in Mumbai Fourteen-year-old Manjunath Kumar knows that he is good at cricket - even if he's not as good as his elder brother Radha. He knows that he fears and resents his cricket-obsessed father. But there are many things about himself and the world that he doesn't know.When he meets Radha's great rival - a boy as privileged and confident as Manju is not - everything in his world begins to change.",
            //            CategoryId = 2, AuthorId = 3, PublisherId = 4, CostPrice = 350.40M, SellingPrice=400.00M, Edition = "1st Edition",
            //            ISBN = "458623",TranslatorId=2,NumberOfPage=564,Language="Bangla",ImageUrl = "none",IsActive = true}
            //        };
            //    book.ForEach(b => context.Books.AddOrUpdate(b));
            //    context.SaveChanges();
            //}
            //if (!context.Stocks.Any())
            //{
            //    var stock = new List<Stock>
            //    {
            //        new Stock { BookId=1, Quantity=10},
            //        new Stock { BookId=2, Quantity=20},
            //        new Stock { BookId=3, Quantity=10},
            //        new Stock { BookId=4, Quantity=30},
            //        new Stock { BookId=1, Quantity=10},
            //        new Stock { BookId=2, Quantity=20},
            //        new Stock { BookId=3, Quantity=10},
            //        new Stock { BookId=2, Quantity=30},
            //        new Stock { BookId=1, Quantity=20},
            //        new Stock { BookId=4, Quantity=30}
            //    };
            //    stock.ForEach(s => context.Stocks.Add(s));
            //    context.SaveChanges();
            //}
            #endregion

            if (!context.Users.Any())
            {
                var user = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        FirstName = "Mamun", LastName = "Bhuiyan",DoB = DateTime.Parse("01/01/1990"),Gender = "Male",Address = "Dhaka",ImageUrl = "None",UserType = "Employee",IsActive = true, Email = "mamun@mail.com",
                        PhoneNumber = "01742793518",UserName = "admin"
                    },
                    new ApplicationUser
                    {
                        FirstName = "Nasir", LastName = "Ahmed",DoB = DateTime.Parse("01/01/1990"),Gender = "Male",Address = "Dhaka",ImageUrl = "None",UserType = "Employee",IsActive = true, Email = "nasir@mail.com",
                        PhoneNumber = "01911788956",UserName = "kaium"
                    },
                    new ApplicationUser
                    {
                        FirstName = "Abdul", LastName = "Kaium",DoB = DateTime.Parse("01/01/1990"),Gender = "Male",Address = "Dhaka",ImageUrl = "None",UserType = "Employee",IsActive = true, Email = "kaium@mail.com",
                        PhoneNumber = "01611457812",UserName = "kaium"
                    },
                   
                    new ApplicationUser
                    {
                        FirstName = "Naznin", LastName = "Akter",DoB = DateTime.Parse("01/01/1990"),Gender = "Female",Address = "Dhaka",ImageUrl = "None",UserType = "Employee",IsActive = true, Email = "naznin@mail.com",
                        PhoneNumber = "01711124578",UserName = "naznin"
                    },
                    new ApplicationUser
                    {
                        FirstName = "Mehedi", LastName = "Hasan",DoB = DateTime.Parse("01/01/1990"),Gender = "Male",Address = "Dhaka",ImageUrl = "None",UserType = "Customer",IsActive = true, Email = "mehedi@mail.com",
                        PhoneNumber = "01911986532",UserName = "mehedi"
                    },

                    new ApplicationUser
                    {
                        FirstName = "Azim", LastName = "Mahmud",DoB = DateTime.Parse("01/01/1990"),Gender = "Male",Address = "Dhaka",ImageUrl = "None",UserType = "Customer",IsActive = true, Email = "azim@mail.com",
                        PhoneNumber = "01911987845",UserName = "mehedi"
                    },
                    new ApplicationUser
                    {
                        FirstName = "Manjurul", LastName = "Alam",DoB = DateTime.Parse("01/01/1990"),Gender = "Male",Address = "Dhaka",ImageUrl = "None",UserType = "Customer",IsActive = true, Email = "monju@mail.com",
                        PhoneNumber = "01911987845",UserName = "monju"
                    },
                    new ApplicationUser
                    {
                        FirstName = "Belal", LastName = "Hussain",DoB = DateTime.Parse("01/01/1990"),Gender = "Male",Address = "Dhaka",ImageUrl = "None",UserType = "Customer",IsActive = true, Email = "belal@mail.com",
                        PhoneNumber = "01911987845",UserName = "belal"
                    },
                    new ApplicationUser
                    {
                        FirstName = "Faisal", LastName = "Abir",DoB = DateTime.Parse("01/01/1990"),Gender = "Male",Address = "Dhaka",ImageUrl = "None",UserType = "Customer",IsActive = true, Email = "faisal@mail.com",
                        PhoneNumber = "01911987845",UserName = "faisal"
                    },
                    new ApplicationUser
                    {
                        FirstName = "Khasru", LastName = "Noman",DoB = DateTime.Parse("01/01/1990"),Gender = "Male",Address = "Dhaka",ImageUrl = "None",UserType = "Customer",IsActive = true, Email = "noamn@mail.com",
                        PhoneNumber = "01911987845",UserName = "noman"
                    },

                };
                var password = new List<string> { "Admin@123", "Nasir@123", "Kaium@123", "Naznin@123", "Mehedi@123", "Azim@123", "Monju@123", "Belal@123", "Faisal@123", "Noman@123" };
                for (int i = 0; i < user.Count; i++)
                {
                    userManager.Create(user[i], password[i]);
                }
            }
            if (!context.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole{Name="Admin"},
                    new IdentityRole{Name="Sales"},
                    new IdentityRole{Name="Purchase"},
                    new IdentityRole{Name="Data Entry"},
                    new IdentityRole{Name="Marketing"},
                    new IdentityRole{Name="Development"},
                    new IdentityRole{Name="HR"},
                };
                foreach (var item in roles)
                {
                    roleManager.Create(item);
                }
            }

    
        }
    }
}


