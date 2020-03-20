using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class CatConversionList
    {
        ArrayList categories { get; set;}
        public CatConversionList()
        {
            ArrayList categoriesList= new ArrayList();
            categoriesList.Add(new ItemCategorie("catGloves", new string[] { "GLOVE" }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catOutdoorClothing", new string[] { "APPAREL" }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catFleece", new string[] { "Fleece", "Fleece;Youth", "Outerwear, Fleece" }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catYouth", new string[] { "Fleece;Youth", "Youth ", "T-Shirts & Activewear/Youth", "T-Shirts & Activewear;Youth" }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catHat", new string[] { "Headwear" }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catOutdoorClothing", new string[] { "Outerwear", "Outerwear, Fleece" }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catPolo", new string[] { "Sport Shirts" }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catActivewear", new string[] { "T-Shirts & Activewear", "T-Shirts & Activewear/Youth", "T-Shirts & Activewear;Youth" }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catPants", new string[] { "..." }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catLongSleeve", new string[] { "..." }, "Vet_TousLesProduits", "Garment"));
            categoriesList.Add(new ItemCategorie("catShirt", new string[] { "Wovens" }, "Vet_TousLesProduits", "Garment"));

            //-------------------------------------------------------------------------------------------------------------

            categoriesList.Add(new ItemCategorie("catNotebook", new string[] { "bullet-stationery-notebooks", "Cahiers de notes & Combinés", "Cahiers de notes Éco", "Combinés cahier de notes/Stylo Éco", "Cahiers de notes (option de personnalisation) Éco", "Blocs-notes", "JOTTERS", "NOTEBOOKS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catInkCartridge", new string[] { "Recharges d'encre" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPen", new string[] { "leeds-pens-metal", "leeds-pens-plasticscripto", "leeds-office-stationerygiftsets","Stylos en métal", "bullet-writinginstruments-metalpens", "bullet-writinginstruments-plasticpens","Stylos en plastique", "Stylos/stylets", "Ensembles de stylos & pousse-mines", "Stylos à bille Éco", "Stylos en aluminium", "Stylos Éco", "Stylos à bille Éco", "Stylos/stylet Éco", "Combinés cahier de notes/Stylo Éco", "Stylos mini", "Stylos bannière", "STYLO À BILLE", "Stylos à bille roulante", "PENS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catJournal", new string[] { "leeds-office-journalbooksnotebooks", "Journaux", "Journaux - non rechargeables", "Journaux  couverture rigide", "Journeaux Éco", "Journaux Éco", "COMBO JOURNAL RECHARGEABLE", "PORTEFEUILLE RECHARGEABLE / ECO CAHIER 3 TROUS / LIANT 3 ANNEAUX" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catHighlighter", new string[] { "Surligneurs", "Surligneurs Éco" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPencil", new string[] { "Pousse-mines" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catDesk_Accessories", new string[] { "leeds-desktop-photoframes",  "leeds-desktop-accessories",     "leeds-desktop", "bullet-desktop", "Accessoires de bureau Éco", "Bureau", "CABLES", "DESKTOP", "DOCUMENT HOLDERS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catFolder", new string[] { "Porte-Documents", "Porte-documents", "Porte-Documents " }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPortfolio", new string[] { "leeds-office-padfolios","bullet-stationery-portfolios", "Pochettes multifonctionnelles", "PADFOLIOS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPencilcase", new string[] { "PENCIL CASES" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBinder", new string[] { "RINGBINDERS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catStickyNote", new string[] { "STICKY NOTES", "bullet-stickynotes" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPhotoAlbum", new string[] { "PHOTO ALBUMS HOLDERS" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catTag", new string[] { "Étiquettes à bagage", "LUGGAGE TAGS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPassportHolder", new string[] { "Porte-passeports", "Organisateurs de passeport" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catTravel", new string[] { "leeds-travelgifts-travelaccessories", "leeds-travelgifts-traveltech", "leeds-travelgifts-utilitykitsorganization", "leeds-safetyauto","Organisateurs de voyage", "bullet-travelgifts-accessories", "bullet-healthandbeauty-personalamenities", "bullet-healthandbeauty-dentalhygiene", "bullet-fitnessrecreation-travel", "POCHETTE DE CÂBLE DE VOYAGE", "Portefeuilles de voyage", "GARMENT BAGS", "LUGGAGE TAGS", "TRAVEL ACCESSORIES", "TRAVEL MUGS" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catPowerbank", new string[] { "leeds-mobiletech-power", "bullet-technology-power", "Chargeurs", "Banques de puissance", "CHARGEUR MURAL", "POWER BANKS" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catHeadphones", new string[] { "Casques d'écoute", "leeds-mobiletech-earbuds", "bullet-technology-earbuds", "Écouteurs", "EARBUDS HEADPHONES" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catSpeakers", new string[] { "Haut-parleurs", "leeds-mobiletech-speakers", "bullet-technology-speakers", "ENCEINTES DE CADRE SONORE", "SPEAKERS" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("Electronics", new string[] { "leeds-mobiletech-smarthome", "leeds-mobiletech-emergingtrends", "leeds-mobiletech-cables-adaptors", "leeds-desktop-mobiletechstandsandorganization", "Les lampes", "bullet-technology-emergingtrends", "bullet-technology-mediastandsandcases", "bullet-technology-accessories", "bullet-technology-cablesadaptors", "TECHNOLOGY ACCESSORIES", "TECHNOLOGY GIFT SETS", "bullet-lighting-keylights" }, "catTechnology", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catComputer", new string[] { "COMPUTER ACCESSORIES" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catClock", new string[] { "CLOCKS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catUsb", new string[] { "FLASH DRIVES", "USB ACCESSORIES", "leeds-memory-flashdrives", "leeds-memory-giftsets" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catFlashlight", new string[] { "leeds-flashlights", "leeds-flashlights-specialty", "FLASHLIGHTS", "bullet-lighting-flashlights" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catLamp", new string[] { "LIGHTING", "bullet-lighting-booklights" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPedometer", new string[] { "PEDOMETERS" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPhone", new string[] { "leeds-mobiletech-vr", "leeds-mobiletech-wirelesscharging", "leeds-mobiletech-cases", "SELFIE STICK", "bullet-technology-tabletphonestands", "Porte-téléphones intelligents", "LENTILLE MOBILE CLIP-ON 3-EN-1", "Lunettes VR", "24h Service Supersonique", "STANDS", "CELL PHONE ACCESSORIES", "PHONE CASES", "SMART PHONE WALLETS" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catStylus", new string[] { "bullet-technology-stylus" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catCamera", new string[] { "leeds-mobiletech-actioncameras" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catDrone", new string[] { "leeds-mobiletech-fullcolorpackaging", "leeds-mobiletech-drones" }, "Electronics", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catTechnology", new string[] { "" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catGym", new string[] { "leeds-outdoorliving-personalwellness", "H&W", "Health", "Gym", "FITNESS", "bullet-fitnessrecreation-sportsfitnesssets" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catYoga", new string[] { "Yoga", "YOGA" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBeach_Accessories", new string[] { "BEACH BALL", "BEACH", "bullet-fitnessrecreation-flyers" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catGolf", new string[] { "GOLF ACCESSORIES", "leeds-outdoorliving-golf", "GOLF BAGS", "GOLF GIFT SETS", "GOLF TOOLS", "GOLF TOWELS", "GOLF UMBRELLAS", "SHOE BAGS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBicycle", new string[] { "BICYCLE ACCESSORIES" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catOutdoor", new string[] { "OUTDOOR ACCESSORIES", "bullet-healthandbeauty-lotionsunscreens", "bullet-foodstorage", "bullet-foodstorage", "bullet-fitnessrecreation-fansmisters", "bullet-fitnessrecreation-summeroutdooritems" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catSports_Accessories", new string[] { "bullet-novelty-noisemakers" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catCardholder", new string[] { "Porte-cartes", "CARD HOLDERS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catKeychain", new string[] { "Porte-clés" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catWallet", new string[] { "leeds-travelgifts-wallets", "Portefeuilles de voyage", "PORTEFEUILLE ZIP" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catTools", new string[] { "leeds-tools-multifunction", "leeds-tools-pocketknives", "leeds-tools-tapemeasures", "leeds-tools-toolkits", "Ruban à mesurer", "TOOL SETS", "bullet-tools" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBinoculars", new string[] { "leeds-outdoorliving-binoculars", "BINOCULARS" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catBottle", new string[] { "leeds-drinkware-sportbottles", "leeds-drinkware-vacuuminsulatedbottles", "leeds-drinkware-glassware", "ACRYLIC", "ALUMINUM", "SILICONE", "STAINLESS STEEL", "TRITAN", "bullet-drinkware-insulatedbottles", "bullet-drinkware-sportsbottles" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catWine_Accessories", new string[] { "leeds-housewares-winecheese", "Accueil", "bullet-entertaining", "CORKSCREWS", "WINE ACCESSORIES", "WINE BAGS", "WINE BOXES", "WINE SETS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBeverageBag", new string[] { "BEVERAGE" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBottleholder", new string[] { "BOTTLE HOLDERS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBottleopener", new string[] { "BOTTLE OPENERS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catMug", new string[] { "CERAMIC MUGS", "leeds-drinkware-ceramic",  "MEDI MUGS", "MIGHTY MUGS", "TRAVEL MUGS", "TRAVEL SETS", "bullet-drinkware-ceramics" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catDrinkware", new string[] { "leeds-drinkware-giftsets", "GLASS", "HOT BEVERAGE DRINKWARE", "bullet-drinkware-specialtydrinkware" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catCoaster", new string[] { "COASTERS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catShakerBottle", new string[] { "SHAKER BOTTLES" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catDrinkSleeve", new string[] { "SLEEVES" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catTumbler", new string[] { "leeds-drinkware-mugstumblers", "TUMBLERS", "bullet-drinkware-tumblersmugs" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catWaterBag", new string[] { "WATER BAGS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catCup", new string[] { "bullet-drinkware-stadiumcups" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catApron", new string[] { "APRONS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBBQ_Accessories", new string[] { "leeds-outdoorliving-bbqpicnic", "BBQ AND PICNIC" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catKitchen", new string[] { "leeds-kitchentools", "leeds-housewares-foodstorage", "leeds-housewares-tabletopserving", "CUTTING BOARDS", "KITCHEN", "KITCHEN GIFT SETS", "UTENSILS AND SERVING SETS", "bullet-kitchentools" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catLunchbag", new string[] { "LUNCH BAGS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catCooler", new string[] { "COOLER SEATS", "FOOD COOLERS", "PICNIC", "leeds-coolers-eventcoolers", "leeds-coolers-lunchcoolers",  "bullet -coolers-event", "bullet-coolers-lunchcoolers" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catSaltShaker", new string[] { "SALT AND PEPPER MILLS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catTeaBox", new string[] { "TEA AND COFFEE" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catLanyard", new string[] { "BADGE AND ID HOLDERS", "bullet-keytagsbadgeholders", "bullet-badgeholders" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catGiftSets", new string[] {"leeds-office-stationerygiftsets", "Ensembles-Cadeaux", "Ensembles Éco", "Ensembles-cadeaux", "TECHNOLOGY GIFT SETS", "ENSEMBLE CADEAU EN 2 PIECES","ENSEMBLE-CADEAU", "BLACK LABEL", "GIFT BAGS", "GOLF GIFT SETS", "KITCHEN GIFT SETS"}, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPackaging", new string[] {"Emballages"}, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catCard", new string[] { "CARDS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catCar", new string[] { "AUTO ACCESSORIES", "bullet-technology-carchargers", "SAFETY", "bullet-safetyandauto" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBlanket", new string[] { "leeds-outdoorliving-blankets", "BLANKETS", "BLANKETS AND SEAT CUSHIONS", "leeds-blankets", "bullet-blankets" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catCarabiner", new string[] { "CARABINERS", "leeds-keychainskeylightscarabiners", "leeds-keychainskeylightscarabiners-keylights" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catChair", new string[] { "CHAIRS", "leeds-outdoorliving-eventseating", "COOLER SEATS", "bullet-outdoorleisure-chairs" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBracelet", new string[] { "CONFERENCE ACCESSORIES", "bullet-sublimation-accessories" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBeauty", new string[] { "COSMETIC AND MANICURE", "leeds-healthbeauty", "bullet-healthandbeauty-sanitizers", "bullet-healthandbeauty-lipbalms", "MANICURE SETS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catUmbrella", new string[] { "CUSTOM UMBRELLAS", "leeds-umbellas-compactfolding", "bullet-umbrellas", "leeds-umbrellas-golf", "leeds-umbrellas-stick", "EXECUTIVE UMBRELLAS", "FOLDING UMBRELLAS", "GOLF UMBRELLAS", "IMPORT", "PATIO BEACH" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catTowel", new string[] { "leeds-towels-beachtowels", "leeds-towels-fitness", "leeds-towels-golftowels", "GOLF TOWELS" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catMicrofiber", new string[] { "MICROFIBER" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catGlass", new string[] { "leeds-outdoorliving-sunglasses", "SUNGLASSES", "bullet-fitnessrecreation-sunglasses" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catToys", new string[] { "TOYS", "leeds-games", "bullet-fitnessrecreation-flyers", "bullet-stuffedanimals" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBall", new string[] { "BEACH BALL" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catStressball", new string[] { "STRESSBALL", "bullet-stressrelievers" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catChristmas", new string[] { "bullet-fitnessrecreation-housewares" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catPetAccessories", new string[] { "bullet-sublimation-petaccessories" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catShoeLaces", new string[] { "bullet-sublimation-shoelaces" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catLighter", new string[] { "leeds-lighters" }, "Promo_TousLesProduits", "article publicitaire"));

            categoriesList.Add(new ItemCategorie("catBag", new string[] { "Bags", "leeds-bags-luggage", "leeds-bags-cinchesslings", "leeds-bags-businessmessenger", "leeds-bags-backpacks", "bullet-bags-paper", "bullet-bags-drawstring", "Sacs", "BRIEFS", "COTTON", "FELT", "FOLDING BAGS", "GARMENT BAGS", "GOLF BAGS", "LAPTOP BAGS", "LAUNDRY BAGS", "ROLLER BAGS", "SLING" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBox", new string[] { "BOX" }, "Promo_TousLesProduits", "article publicitaire"));
            categoriesList.Add(new ItemCategorie("catBackpack", new string[] { "leeds-bags-backpacks", "Sac à dos", "Cordon", "BACKPACKS", "CINCHES", "ROLLER BACKPACKS", "SLING", "bullet-bags-backpacks" }, "catBag", ""));
            categoriesList.Add(new ItemCategorie("catTotebag", new string[] { "leeds-bags-totes", "Fourre-Tout", "bullet-bags-totes", "Fourre-Tout ", "COTTON", "FELT", "FOLDING BAGS", "JUTE", "NON WOVEN", "ORGANIC COTTON", "P.E.T.", "PAPER TOTES", "PHTHALATE FREE", "POLYESTER", "STONE PAPER", "WOVEN LAMINATED"  }, "catBag", ""));
            categoriesList.Add(new ItemCategorie("catMessengerBag", new string[] { "leeds-bags-businessmessenger", "MESSENGER BAGS" }, "catBag", ""));
            categoriesList.Add(new ItemCategorie("catShoeBag", new string[] { "SHOE BAGS" }, "catBag", ""));
            categoriesList.Add(new ItemCategorie("catFannyPack", new string[] { "WAIST PACKS", "bullet-bags-fannypacks" }, "catBag", ""));
            categoriesList.Add(new ItemCategorie("catBriefcase", new string[] { "bullet-bags-businesscases" }, "catBag", ""));
            categoriesList.Add(new ItemCategorie("catDuffle", new string[] { "leeds-duffels-sport", "leeds-duffles-travel", "leeds-bags-duffels", "bullet-duffels-sport", "bullet-bags-duffels", "Duffle", "20-22 SPORTS BAGS AND DUFFLES", "23 SPORTS BAGS AND DUFFLES", "24 SPORTS BAGS AND DUFFLES",  "SMALL SPORTS BAGS AND DUFFLES" }, "catBag", ""));
            categories = categoriesList;
        }

        public void checkCategories(String input, ArrayList listeCategories)
        {
            for(int i = 0; i < this.categories.Count; i++)
            {
                ItemCategorie categorie = (ItemCategorie)this.categories[i];
                for (int j = 0; j < categorie.motsCategorie.Length; j++)
                {
                    if (input.Equals(categorie.motsCategorie[j]))
                    {
                        addIfNotThere(categorie.nomCategorie, listeCategories);
                        addIfNotThere(categorie.categorieParent, listeCategories);
                    }
                    else if (input.Equals(categorie.nomCategorie))
                    {
                        addIfNotThere(categorie.nomCategorie, listeCategories);
                        addIfNotThere(categorie.categorieParent, listeCategories);
                    }
                }
            }
        }

        public void debugCategories(String input, StreamWriter file, ArrayList categoriesManquantes)
        {
            
            Boolean exists = false;
            for (int i = 0; i < this.categories.Count; i++)
            {
                ItemCategorie categorie = (ItemCategorie)this.categories[i];
                for (int j = 0; j < categorie.motsCategorie.Length; j++)
                {
                    if (input.Equals(categorie.motsCategorie[j]))
                    {
                        exists = true;
                        break;
                    }
                }
                if (exists)
                {
                    break;
                }
            }
            if (!exists)
            {
                exists = checkIfThere(input, categoriesManquantes);
                if(!exists)
                    file.WriteLine(input);
            }
        }

        public Boolean checkIfThere(String categorie, ArrayList listeCategories)
        {
            for (int i = 0; i < listeCategories.Count; i++)
            {
                if (categorie.Equals((string)listeCategories[i]))
                {
                    return true;
                }
            }
            listeCategories.Add(categorie);
            return false;
        }

        public void addIfNotThere(String categorie, ArrayList listeCategories)
        {
            for(int i = 0; i < listeCategories.Count; i++)
            {
                if (categorie.Equals((string)listeCategories[i]))
                {
                    return;
                }
            }
            listeCategories.Add(categorie);
        }

        public static String getCategoryContext(String category, CatConversionList conversionList)
        {
            ArrayList categoryList = conversionList.categories;
            for(int i = 0; i < categoryList.Count; i++)
            {
                ItemCategorie categoryCheck = (ItemCategorie)categoryList[i];
                if (category.Equals(categoryCheck.nomCategorie))
                {
                    if (categoryCheck.categorieContext.Equals(""))
                    {
                        return getCategoryContext(categoryCheck.categorieParent, conversionList);
                    }
                    else
                    {
                        return categoryCheck.categorieContext;
                    }
                }
            }
            return "N/A";
        }
    }
}
