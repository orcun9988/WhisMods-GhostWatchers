using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Donteco;
using Whis;
using ESP_Models;

namespace Whis
{
    class ESP
    {
        public static bool lon = false;

        public static void show_esp()
        {


            if (Main.Main.ghostesp)
            {
                ghostboxESP();
            }
            if (Main.Main.ghostbone)
            {
                ghostboneESP();
            }
 

        }

        private static void ghostboxESP()
        {
            float distance = Vector3.Distance(Main.Main.localplayer.transform.position, Main.Main.ghost.transform.position);
            int distanceToint = (int)distance;

            Vector3 w2s_head = Camera.main.WorldToScreenPoint(Main.Main.ghost.transform.position);

            Vector3 pivotPos1 = Main.Main.ghost.transform.position;
            Vector3 playerFootPos1; playerFootPos1.x = pivotPos1.x; playerFootPos1.z = pivotPos1.z; playerFootPos1.y = pivotPos1.y + 0.08f;
            Vector3 playerHeadPos1; playerHeadPos1.x = playerFootPos1.x; playerHeadPos1.z = playerFootPos1.z; playerHeadPos1.y = playerFootPos1.y + Main.Main.ghost.Movement.GetHeight();

            Vector3 w2s_playerFoot1 = Camera.main.WorldToScreenPoint(playerFootPos1);
            Vector3 w2s_playerHead1 = Camera.main.WorldToScreenPoint(playerHeadPos1);

            if (w2s_head.z >= 0)
                DrawESP(w2s_playerFoot1, w2s_playerHead1, Color.red, Main.Main.ghost.Data.GhostType + " " + Main.Main.ghost.Data.Age + " [" + distanceToint + "m]");
            // GUI.Label(new Rect(w2s_head.x, (float)UnityEngine.Screen.height - w2s_head.y, 100f, 100f), ghost.Data.GhostType + "-" + ghost.Data.Age + "| [" + distanceToint + "m]");//Name Esp
        }







        private static void ghostboneESP()
        {
            switch (Main.Main.ghost.Data.name.Replace("(Clone)", "").ToLower().Replace(" ", ""))
            {
                case "poltergeist":
                    ESP_Models.Poltergeist.show_bones();
                    break;
                case "vampire":
                    ESP_Models.Vampire.show_bones();
                    break;
                case "demon":
                    ESP_Models.Demon.show_bones();
                    break;
                case "gallowsghost":
                    ESP_Models.GallowsGhost.show_bones();
                    break;
                case "drowned":
                    ESP_Models.Drowned.show_bones();
                    break;
                case "baby":
                    ESP_Models.Baby.show_bones();
                    break;
                case "darkness":
                    ESP_Models.Darkness.show_bones();
                    break;
                case "puppet":
                    ESP_Models.Puppet.show_bones();
                    break;
            }
        }




        private static bool IsOnScreen(Transform ent_transform)
        {
            Vector3 w2s_check = Camera.main.WorldToScreenPoint(ent_transform.position);

            if (w2s_check.z > 0.1f && w2s_check.x > 0f && w2s_check.x < (float)Screen.width && w2s_check.y > 0 && w2s_check.y < (float)Screen.height)
            {
                return true;
            }

            return false;
        }

        public static void DrawESP(Vector3 objfootPos, Vector3 objheadPos, Color objColor, String name)
        {
            float height = objheadPos.y - objfootPos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;

            Render.DrawBox2(objfootPos.x - (width / 2), (float)Screen.height - objfootPos.y - height, width, height, objColor, 1f);
            Render.DrawString(new Vector2(objfootPos.x - (width / 2), (float)Screen.height - objfootPos.y - height), $"{name}", Color.green);
        }

        public static void DrawBoneLine(Vector3 w2s_objectStart, Vector3 w2s_objectFinish, Color color)
        {
            Vector3 w2s_head = Camera.main.WorldToScreenPoint(Main.Main.ghost.transform.position);
            if (w2s_head.z >  0)

                if (w2s_objectStart != null && w2s_objectFinish != null)
            {
                Render.DrawLine(new Vector2(w2s_objectStart.x, (float)Screen.height - w2s_objectStart.y), new Vector2(w2s_objectFinish.x, (float)Screen.height - w2s_objectFinish.y), color, 1f);
            }
        }
    }
}
