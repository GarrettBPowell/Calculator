using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {

        string top = "";
        string[] content = { "", "---------------", "" };

        public Form1()
        {            
            InitializeComponent();
        }

        //updates current text content
        public void Update()
        {
            content[0] = top;
            calcDisplay.Lines = content;
        }

        //all of these are buttons that add the character they represent to the top of the calculator
        private void button1_Click(object sender, EventArgs e)
        {
            top += "1";
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            top += "2";
            Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            top += "3";
            Update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            top += "4";
            Update();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            top += "5";
            Update();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            top += "6";
            Update();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            top += "7";
            Update();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            top += "8";
            Update();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            top += "9";
            Update();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            top += "0";
            Update();
        }

        private void buttonAC_Click(object sender, EventArgs e)
        {
            top = "";
            content[2] = "";
            Update();
        }

        private void PosNeg_Click(object sender, EventArgs e)
        {
            top += '-';
            Update();
        }

        private void percent_Click(object sender, EventArgs e)
        {
            if (!(top.Length == 0))
            {
                if (!(top[top.Length - 1] == '+' || top[top.Length - 1] == '*' || top[top.Length - 1] == '/' || top[top.Length - 1] == '%' || top[top.Length - 1] == '-'))
                    top += "%";
                Update();
            }
        }
        private void mult_Click(object sender, EventArgs e)
        {
            if (!(top.Length == 0))
            {
                if (!(top[top.Length - 1] == '+' || top[top.Length - 1] == '*' || top[top.Length - 1] == '/' || top[top.Length - 1] == '%' || top[top.Length - 1] == '-'))
                    top += "*";
            }
            Update();
        }

        private void minus_Click(object sender, EventArgs e)
        {    
            top += "-";
            Update();
        }

        private void plus_Click(object sender, EventArgs e)
        {
            if (!(top.Length == 0))
            {
                if (!(top[top.Length - 1] == '+' || top[top.Length - 1] == '*' || top[top.Length - 1] == '/' || top[top.Length - 1] == '%' || top[top.Length - 1] == '-'))
                    top += "+";
            }
            Update();
        }

        private void divide_Click(object sender, EventArgs e)
        {
            if (!(top.Length == 0))
            {
                if (!(top[top.Length - 1] == '+' || top[top.Length - 1] == '*' || top[top.Length - 1] == '/' || top[top.Length - 1] == '%' || top[top.Length - 1] == '-'))
                    top += "/";
            }
            Update();
        }

        private void dec_Click(object sender, EventArgs e)
        {
            top += ".";
            Update();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void calcDisplay_TextChanged(object sender, EventArgs e)
        {
            
        }

        //this function formats the negative on the number to either get the even number of '-' and make it a positive number 
        //or return a neg number with only 1 '-' in front
        private string formatNeg(string checkNeg)
        {
            int numOfNeg = 0;
            while (checkNeg[numOfNeg] == '-')
                numOfNeg++;

            //if no '-'
            if (numOfNeg == 0)
                return checkNeg;
            //if odd number of '-'
            else if (numOfNeg % 2 == 1)
                checkNeg = checkNeg.Substring(numOfNeg - 1);
            //if even number of '-'
            else
                checkNeg = checkNeg.Substring(numOfNeg);

            return checkNeg;
        }

        private void equals_Click(object sender, EventArgs e)
        {
            //mult
            for (int i = 0; i < top.Length; i++)
            {
                if (top[i] == '*')
                {
                    int positionOfMult = i;
                    int begOfNum = i;
                    int endOfNum = i;

                    //find num in front of * to mult
                    do
                    {
                        begOfNum--;
                    } while (top[begOfNum] != '+' && top[begOfNum] != '/' && top[begOfNum] != '*' && top[begOfNum] != '%' && begOfNum != 0);

                    //if the position is at 0, -1 to adjust for substring
                    if (begOfNum == 0)
                        begOfNum--;

                    //check if the num is negative and if so how many signs
                    String checkNeg = top.Substring(1 + begOfNum, positionOfMult - begOfNum - 1);

                    //save first num
                    double numToMult = Convert.ToDouble(formatNeg(checkNeg));

                    //find num behind * to mult
                    do
                    {
                        endOfNum++;
                    } while (top[endOfNum] != '+' && top[endOfNum] != '/' && top[endOfNum] != '*' && top[endOfNum] != '%' && endOfNum != top.Length - 1);

                    //check if second num is neg
                    //if it is the last number do this
                    if (endOfNum == top.Length - 1)
                        checkNeg = top.Substring(positionOfMult + 1);
                    //otherwise do this substring
                    else
                        checkNeg = top.Substring(positionOfMult + 1, endOfNum - positionOfMult - 1);

                    //save second num
                    double otherNumToMult = Convert.ToDouble(formatNeg(checkNeg));
                    
                    //do actual math
                    double change = numToMult * otherNumToMult;

                    //change content of top according to what was just done
                    if (endOfNum == top.Length - 1)
                        top = top.Substring(0, begOfNum + 1) + change;
                    else
                        top = top.Substring(0, begOfNum + 1) + change + top.Substring(endOfNum);
                    i = 0;
                }
            }

            //div
            for (int i = 0; i < top.Length; i++)
            {
                if (top[i] == '/')
                {
                    int positionOfDiv = i;
                    int begOfNum = i;
                    int endOfNum = i;

                    //find num in front of / to div
                    do
                    {
                        begOfNum--;
                    } while (top[begOfNum] != '+' && top[begOfNum] != '/' && top[begOfNum] != '%' && begOfNum != 0);

                    //if the position is at 0, -1 to adjust for substring
                    if (begOfNum == 0)
                        begOfNum--;

                    //check if the num is negative and if so how many signs
                    String checkNeg = top.Substring(1 + begOfNum, positionOfDiv - begOfNum - 1);

                    //save first num
                    double numToDiv = Convert.ToDouble(formatNeg(checkNeg));

                    //find num behind / to div
                    do
                    {
                        endOfNum++;
                    } while (top[endOfNum] != '+' && top[endOfNum] != '/' && top[endOfNum] != '%' && endOfNum != top.Length - 1);

                    //check if second num is neg
                    //if it is the last number do this
                    if (endOfNum == top.Length - 1)
                        checkNeg = top.Substring(positionOfDiv + 1);
                    //otherwise do this substring
                    else
                        checkNeg = top.Substring(positionOfDiv + 1, endOfNum - positionOfDiv - 1);

                    //save second num
                    double otherNumToDiv = Convert.ToDouble(formatNeg(checkNeg));
                    double change = 0.0;
                    //do actual math
                    if (otherNumToDiv == 0)
                    {
                        top = "ERROR";
                        break;
                    }
                    else
                        change = numToDiv / otherNumToDiv;

                    //change content of top according to what was just done
                    if (endOfNum == top.Length - 1)
                        top = top.Substring(0, begOfNum + 1) + change;
                    else
                        top = top.Substring(0, begOfNum + 1) + change + top.Substring(endOfNum);
                    i = 0;
                }
            }

            //mod
            for (int i = 0; i < top.Length; i++)
            {
                if (top[i] == '%')
                {
                    int positionOfMod = i;
                    int begOfNum = i;
                    int endOfNum = i;

                    //find num in front of % to mod
                    do
                    {
                        begOfNum--;
                    } while (top[begOfNum] != '+' && top[begOfNum] != '%' && begOfNum != 0);

                    //if the position is at 0, -1 to adjust for substring
                    if (begOfNum == 0)
                        begOfNum--;

                    //check if the num is negative and if so how many signs
                    String checkNeg = top.Substring(1 + begOfNum, positionOfMod - begOfNum - 1);

                    //save first num
                    double numToMod = Convert.ToDouble(formatNeg(checkNeg));

                    //find num behind % to mod
                    do
                    {
                        endOfNum++;
                    } while (top[endOfNum] != '+' && top[endOfNum] != '%' && endOfNum != top.Length - 1);

                    //check if second num is neg
                    //if it is the last number do this
                    if (endOfNum == top.Length - 1)
                        checkNeg = top.Substring(positionOfMod + 1);
                    //otherwise do this substring
                    else
                        checkNeg = top.Substring(positionOfMod + 1, endOfNum - positionOfMod - 1);

                    //save second num
                    double otherNumToMod = Convert.ToDouble(formatNeg(checkNeg));

                    //do actual math
                    double change = numToMod % otherNumToMod;

                    //change content of top according to what was just done
                    if (endOfNum == top.Length - 1)
                        top = top.Substring(0, begOfNum + 1) + change;
                    else
                        top = top.Substring(0, begOfNum + 1) + change + top.Substring(endOfNum);
                    i = 0;
                }
            }

            //plus
            for (int i = 0; i < top.Length; i++)
            {
                if (top[i] == '+')
                {
                    int positionOfAdd = i;
                    int begOfNum = i;
                    int endOfNum = i;

                    //find num in front of + to add
                    do
                    {
                        begOfNum--;
                    } while (top[begOfNum] != '+' && begOfNum != 0);

                    //if the position is at 0, -1 to adjust for substring
                    if (begOfNum == 0)
                        begOfNum--;

                    //check if the num is negative and if so how many signs
                    String checkNeg = top.Substring(1 + begOfNum, positionOfAdd - begOfNum - 1);

                    //save first num
                    double numToAdd = Convert.ToDouble(formatNeg(checkNeg));

                    //find num behind + to add
                    do
                    {
                        endOfNum++;
                    } while (top[endOfNum] != '+' && endOfNum != top.Length - 1);

                    //check if second num is neg
                    //if it is the last number do this
                    if (endOfNum == top.Length - 1)
                        checkNeg = top.Substring(positionOfAdd + 1);
                    //otherwise do this substring
                    else
                        checkNeg = top.Substring(positionOfAdd + 1, endOfNum - positionOfAdd - 1);

                    //save second num
                    double otherNumToAdd = Convert.ToDouble(formatNeg(checkNeg));

                    //do actual math
                    double change = numToAdd + otherNumToAdd;

                    //change content of top according to what was just done
                    if (endOfNum == top.Length - 1)
                        top = top.Substring(0, begOfNum + 1) + change;
                    else
                        top = top.Substring(0, begOfNum + 1) + change + top.Substring(endOfNum);
                    i = 0;
                }
            }

            //minus
            for (int i = 1; i < top.Length; i++)
            {
                if (top[i] == '-' && top[i-1] != '-')
                {
                    int positionOfSub = i;
                    int begOfNum = i;
                    int endOfNum = i;

                    //find num in front of + to add
                    int firstNeg = 0;
                    do
                    {
                        begOfNum--;
                    } while (!(top[begOfNum] != '-' && top[begOfNum + (firstNeg++)] == '-') && begOfNum != 0);

                    //if the position is at 0, -1 to adjust for substring
                    if (begOfNum == 0)
                        begOfNum--;

                    //check if the num is negative and if so how many signs
                    String checkNeg = top.Substring(1 + begOfNum, positionOfSub - begOfNum - 1);

                    //save first num
                    double numToSub = Convert.ToDouble(formatNeg(checkNeg));

                    //find num behind + to add
                    firstNeg = 0;
                    do
                    {
                        endOfNum++;
                    } while (!(top[endOfNum] != '-' && top[endOfNum + (firstNeg++)] == '-') && endOfNum != top.Length - 1);

                    //check if second num is neg
                    //if it is the last number do this
                    if (endOfNum == top.Length - 1)
                        checkNeg = top.Substring(positionOfSub + 1);
                    //otherwise do this substring
                    else
                        checkNeg = top.Substring(positionOfSub + 1, endOfNum - positionOfSub - 1);

                    //save second num
                    double otherNumToSub = Convert.ToDouble(formatNeg(checkNeg));

                    //do actual math
                    double change = numToSub - otherNumToSub;

                    //change content of top according to what was just done
                    if (endOfNum == top.Length - 1)
                        top = top.Substring(0, begOfNum + 1) + change;
                    else
                        top = top.Substring(0, begOfNum + 1) + change + top.Substring(endOfNum);
                    i = 0;
                }
            }

            //take content and display the correct answer in the bottom
            content[2] = Convert.ToString(top);
            calcDisplay.Lines = content;
        }
    }
}
