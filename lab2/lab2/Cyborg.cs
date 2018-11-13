﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Cyborg : Robot
    {
        private int charge = 100;
        private int weight = 0;
        private int cost = 0;
        private int package_flag = 0;
        private int i = 0;
        private int middle = 0;
        Guid unique = Guid.NewGuid();
        string Legend = "History:\n" +
            "avatar:-\nname: Cyborg\n" +
            "vulnerabilities: can burn, get sick after 28 steps\n" +
            "opportunities: max weight 35 kg, charge - 100%\n" +
            "decoding: 60%";
        string Flag_legend = "0";
        public Cyborg()
        {
            Console.WriteLine("Unique code: " + unique);
            Console.WriteLine("Your bot is cyborg");
        }
        public Cyborg(string flag_legend)
        {
            Flag_legend = flag_legend;
        }
        public string RobotLegend()
        {
            string output_legend = "0";
            switch (Flag_legend)
            {
                case "h":
                    output_legend = Legend;
                    break;
            }
            return output_legend;
        }
        public string LostEnergy()
        {
            string result = "0";
            if (charge==0)
            {
                result = "0";
                return result;
            }
            else
            {
                charge--;
            }
            result = "Your weight: " + weight + " , charge: " + charge ;
            return result;
        }
        public string LoadCapacity()
        {
            string result = "0";
            int decode_flag = 0;
            int bad_flag = 0;
            double bad_state = 0;
            Package package = new Package();
            Random random = new Random();
            package_flag = random.Next(1, 4);
            
            switch (package_flag)
            {
                case 1:
                    BadPackage badPackage = new BadPackage();
                    bad_flag = badPackage.TakeBadPackage();
                    if (bad_flag == 1)
                    {
                        result = "Sorry package is bad";
                        return result;
                    }
                    break;
                case 2:
                    DecoderPackage decoder = new DecoderPackage();
                    decode_flag = decoder.Decoding(3);
                    if (decode_flag == 1)
                    {
                        result = "Decoding is successful";
                        break;
                    }
                    else
                    {
                        result = "Decoding is not success";
                        return result;
                    }
                    
                case 3:
                    DangerPackage danger = new DangerPackage();
                    charge = danger.TakeCharge(charge);
                    result = "Danger package: your charge " + charge;
                    return result;
            }
            
            if (package.Weight() > 35)
            {
                result = "Your weight more than 35 kg";
            }        
            else
            {
                i++;
                if (package.Weight() <= 5)
                {
                    charge -= 2;
                }
                else if (package.Weight() <= 20)
                {
                    charge -= 4;
                }
                else
                {
                    charge -= 5;
                }
                weight = weight + package.Weight();
              
            }
            middle = weight / i;
            if(middle >= 28)
            {
                Random rand = new Random();
                bad_state = rand.NextDouble();
                if (bad_state < 0.3)
                {
                    charge -= 10;
                }            
            }
            if (charge <= 0)
            {
                charge = 0;
                result = "0";
            }
            else
            {
                result = "Your weight: " + weight + " , charge: " + charge + TotalCost();
            }
            return result;
        }
        public string TotalCost()
        {
            string result = "0";
            Package package = new Package();
            cost = cost + package.Cost();
            result =" ,cost: " + cost;
            return result;
        }

        public RobotMemento SaveState()
        {
            return new RobotMemento(charge, weight, cost);
        }
        public string RestoreState(RobotMemento memento)
        {
            string result = "0";
            charge = memento.Battery_charge;
            weight = memento.Weight_package;
            cost = memento.Cost_package;
            result ="Restore game: charge - "+charge+ ", weight - "+ weight +" ,cost - " + cost;
            return result;
        }
    }
}