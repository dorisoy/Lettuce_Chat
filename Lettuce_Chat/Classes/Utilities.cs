﻿using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lettuce_Chat.Classes
{
    public static class Utilities
    {
        public static string RootPath { get; set; }
        private static string VegetableList { get; } = @"[
    ""Artichoke"",
    ""Arugula"",
    ""Asparagus"",
    ""Aubergine"",
    ""Aubergine"",
    ""Legumes"",
    ""Alfalfa"",
    ""Azuki Bean"",
    ""Sprouting"",
    ""Black Bean"",
    ""Black-eyed Peas"",
    ""Borlotti Bean"",
    ""Fava Bean"",
    ""Chickpea"",
    ""Green Bean"",
    ""Kidney Bean"",
    ""Lentil"",
    ""Lima Bean"",
    ""Mung Bean"",
    ""Navy Bean"",
    ""Pinto Bean"",
    ""Runner Bean"",
    ""Split Peas"",
    ""Soy Bean"",
    ""Pea"",
    ""Mangetout"",
    ""Snap Peas"",
    ""Bok Choy"",
    ""Broccoflower"",
    ""Broccoli"",
    ""Brussels Sprout"",
    ""Cabbage"",
    ""Calabrese"",
    ""Carrots"",
    ""Cauliflower"",
    ""Celery"",
    ""Chard"",
    ""Collard Greens"",
    ""Corn Salad"",
    ""Endive"",
    ""Fiddleheads"",
    ""Frisee"",
    ""Fennel"",
    ""Herbs"",
    ""Anise"",
    ""Basil"",
    ""Caraway"",
    ""Coriander"",
    ""Chamomile"",
    ""Dill"",
    ""Fennel"",
    ""Lavender"",
    ""Lemon Grass"",
    ""Marjoram"",
    ""Oregano"",
    ""Parsley"",
    ""Rosemary"",
    ""Sage"",
    ""Thyme"",
    ""Kale"",
    ""Kohlrabi"",
    ""Lettuce"",
    ""Maize"",
    ""Maize"",
    ""Sweetcorn"",
    ""Poaceae"",
    ""Mushrooms"",
    ""Fungus"",
    ""Plant"",
    ""Mustard Greens"",
    ""Nettle"",
    ""Okra"",
    ""Chives"",
    ""Garlic"",
    ""Leek"",
    ""Onion"",
    ""Shallot"",
    ""Spring Onion"",
    ""Green Onion"",
    ""Scallion"",
    ""Parsley"",
    ""Capsicum"",
    ""Green Pepper"",
    ""Red Pepper"",
    ""Bell Pepper"",
    ""Pimento"",
    ""Chili Pepper"",
    ""Capsicum"",
    ""Jalapeño"",
    ""Habanero"",
    ""Paprika"",
    ""Tabasco Pepper"",
    ""Cayenne Pepper"",
    ""Radicchio"",
    ""Rhubarb"",
    ""Root Vegetable"",
    ""Beetroot"",
    ""Beet"",
    ""Mangel-wurzel"",
    ""Carrot"",
    ""Celeriac"",
    ""Daikon"",
    ""Ginger"",
    ""Parsnip"",
    ""Rutabaga"",
    ""Turnip"",
    ""Radish"",
    ""Rutabaga"",
    ""Rutabaga"",
    ""Turnip"",
    ""Wasabi"",
    ""Horseradish"",
    ""White Radish"",
    ""Salsify"",
    ""Purple Salsify"",
    ""Skirret"",
    ""Spinach"",
    ""Courgette"",
    ""Zucchini"",
    ""Cucumber"",
    ""Delicata"",
    ""Squash"",
    ""Patty Pans"",
    ""Pumpkin"",
    ""Tat Soi"",
    ""Tomato"",
    ""Rhizome"",
    ""Jicama"",
    ""Potato"",
    ""Sunchokes"",
    ""Sweet Potato"",
    ""Taro"",
    ""Yam"",
    ""Water Chestnut"",
    ""Watercress"",
    ""Zucchini""
]";
        private static string AdjectiveList { get; } = @"[
    ""Adorable"",
    ""Adventurous"",
    ""Aggressive"",
    ""Agreeable"",
    ""Alert"",
    ""Alive"",
    ""Amused"",
    ""Angry"",
    ""Annoyed"",
    ""Annoying"",
    ""Anxious"",
    ""Arrogant"",
    ""Ashamed"",
    ""Attractive"",
    ""Average"",
    ""Awful"",
    ""Bad"",
    ""Beautiful"",
    ""Better"",
    ""Bewildered"",
    ""Black"",
    ""Bloody"",
    ""Blue"",
    ""Blue-eyed"",
    ""Blushing"",
    ""Bored"",
    ""Brainy"",
    ""Brave"",
    ""Breakable"",
    ""Bright"",
    ""Busy"",
    ""Calm"",
    ""Careful"",
    ""Cautious"",
    ""Charming"",
    ""Cheerful"",
    ""Clean"",
    ""Clear"",
    ""Clever"",
    ""Cloudy"",
    ""Clumsy"",
    ""Colorful"",
    ""Combative"",
    ""Comfortable"",
    ""Concerned"",
    ""Condemned"",
    ""Confused"",
    ""Cooperative"",
    ""Courageous"",
    ""Crazy"",
    ""Creepy"",
    ""Crowded"",
    ""Cruel"",
    ""Curious"",
    ""Cute"",
    ""Dangerous"",
    ""Dark"",
    ""Dead"",
    ""Defeated"",
    ""Defiant"",
    ""Delightful"",
    ""Depressed"",
    ""Determined"",
    ""Different"",
    ""Difficult"",
    ""Disgusted"",
    ""Distinct"",
    ""Disturbed"",
    ""Dizzy"",
    ""Doubtful"",
    ""Drab"",
    ""Dull"",
    ""Eager"",
    ""Easy"",
    ""Elated"",
    ""Elegant"",
    ""Embarrassed"",
    ""Enchanting"",
    ""Encouraging"",
    ""Energetic"",
    ""Enthusiastic"",
    ""Envious"",
    ""Evil"",
    ""Excited"",
    ""Expensive"",
    ""Exuberant"",
    ""Fair"",
    ""Faithful"",
    ""Famous"",
    ""Fancy"",
    ""Fantastic"",
    ""Fierce"",
    ""Filthy"",
    ""Fine"",
    ""Foolish"",
    ""Fragile"",
    ""Frail"",
    ""Frantic"",
    ""Friendly"",
    ""Frightened"",
    ""Funny"",
    ""Gentle"",
    ""Gifted"",
    ""Glamorous"",
    ""Gleaming"",
    ""Glorious"",
    ""Good"",
    ""Gorgeous"",
    ""Graceful"",
    ""Grieving"",
    ""Grotesque"",
    ""Grumpy"",
    ""Handsome"",
    ""Happy"",
    ""Healthy"",
    ""Helpful"",
    ""Helpless"",
    ""Hilarious"",
    ""Homeless"",
    ""Homely"",
    ""Horrible"",
    ""Hungry"",
    ""Hurt"",
    ""Ill"",
    ""Important"",
    ""Impossible"",
    ""Inexpensive"",
    ""Innocent"",
    ""Inquisitive"",
    ""Itchy"",
    ""Jealous"",
    ""Jittery"",
    ""Jolly"",
    ""Joyous"",
    ""Kind"",
    ""Lazy"",
    ""Light"",
    ""Lively"",
    ""Lonely"",
    ""Long"",
    ""Lovely"",
    ""Lucky"",
    ""Magnificent"",
    ""Misty"",
    ""Modern"",
    ""Motionless"",
    ""Muddy"",
    ""Mushy"",
    ""Mysterious"",
    ""Nasty"",
    ""Naughty"",
    ""Nervous"",
    ""Nice"",
    ""Nutty"",
    ""Obedient"",
    ""Obnoxious"",
    ""Odd"",
    ""Old-fashioned"",
    ""Open"",
    ""Outrageous"",
    ""Outstanding"",
    ""Panicky"",
    ""Perfect"",
    ""Plain"",
    ""Pleasant"",
    ""Poised"",
    ""Poor"",
    ""Powerful"",
    ""Precious"",
    ""Prickly"",
    ""Proud"",
    ""Puzzled"",
    ""Quaint"",
    ""Real"",
    ""Relieved"",
    ""Repulsive"",
    ""Rich"",
    ""Scary"",
    ""Selfish"",
    ""Shiny"",
    ""Shy"",
    ""Silly"",
    ""Sleepy"",
    ""Smiling"",
    ""Smoggy"",
    ""Sore"",
    ""Sparkling"",
    ""Splendid"",
    ""Spotless"",
    ""Stormy"",
    ""Strange"",
    ""Stupid"",
    ""Successful"",
    ""Super"",
    ""Talented"",
    ""Tame"",
    ""Tender"",
    ""Tense"",
    ""Terrible"",
    ""Testy"",
    ""Thankful"",
    ""Thoughtful"",
    ""Thoughtless"",
    ""Tired"",
    ""Tough"",
    ""Troubled"",
    ""Ugliest"",
    ""Ugly"",
    ""Uninterested"",
    ""Unsightly"",
    ""Unusual"",
    ""Upset"",
    ""Uptight"",
    ""Vast"",
    ""Victorious"",
    ""Vivacious"",
    ""Wandering"",
    ""Weary"",
    ""Wicked"",
    ""Wide-eyed"",
    ""Wild"",
    ""Witty"",
    ""Worrisome"",
    ""Worried"",
    ""Wrong"",
    ""Zany"",
    ""Zealous""
]";
        public static string GetRandomUserName()
        {
            var seed = new Random().Next(0, 100);
            string[] adjectives = JsonConvert.DeserializeObject<string[]>(AdjectiveList);
            var adjective = adjectives[new Random(seed).Next(0, adjectives.Length - 1)];
            string[] veggies = JsonConvert.DeserializeObject<string[]>(VegetableList);
            var veggie = veggies[new Random(seed).Next(0, veggies.Length - 1)];
            return $"{adjective} {veggie}";
        }
        public static string GetRandomChatName()
        {
            var seed = new Random().Next(100, 200);
            string[] adjectives = JsonConvert.DeserializeObject<string[]>(AdjectiveList);
            var adjective = adjectives[new Random(seed).Next(0, adjectives.Length - 1)];
            string[] veggies = JsonConvert.DeserializeObject<string[]>(VegetableList);
            var veggie = veggies[new Random(seed).Next(0, veggies.Length - 1)];
            return $"{adjective} {veggie} Chat";
        }
        // Remove trailing empty bytes in the buffer.
        public static byte[] TrimBytes(byte[] bytes)
        {
            // Loop backwards through array until the first non-zero byte is found.
            var firstZero = 0;
            for (int i = bytes.Length - 1; i >= 0; i--)
            {
                if (bytes[i] != 0)
                {
                    firstZero = i + 1;
                    break;
                }
            }
            if (firstZero == 0)
            {
                throw new Exception("Byte array is empty.");
            }
            // Return non-empty bytes.
            return bytes.Take(firstZero).ToArray();
        }
        public static void WriteToLog(Exception Ex)
        {
            var exception = Ex;
            while (exception != null)
            {
                var entry = new
                {
                    Type = "Error",
                    Timestamp = DateTime.Now,
                    Message = exception.Message,
                    Source = exception.Source,
                    Stack = exception.StackTrace
                };
                File.WriteAllText(JsonConvert.SerializeObject(entry) + Environment.NewLine, RootPath + $@"\Data\Logs\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}.txt");
                exception = exception.InnerException;
            }

        }
        public static void WriteToLog(string Message)
        {
            var entry = new
            {
                Type = "Info",
                Timestamp = DateTime.Now,
                Message = Message
            };
            File.WriteAllText(JsonConvert.SerializeObject(entry) + Environment.NewLine, RootPath + $@"\Data\Logs\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}.txt");
        }
        public enum Permanence
        {
            Temporary,
            Permanent
        }
    }
}
