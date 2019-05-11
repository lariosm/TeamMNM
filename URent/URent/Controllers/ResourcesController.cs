﻿using Microsoft.AspNet.Identity;
using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using URent.Models;

namespace URent.Controllers
{
    public class ResourcesController : Controller
    {
        SUPContext db = new SUPContext();

        [Authorize]
        private string getIdentityID()
        {
            return User.Identity.GetUserId();
        }

        /// <summary>
        /// Retrieves user ID of current user from SUPUsers table that is associated with user ID from AspNetUsers table.
        /// </summary>
        /// <returns>User ID of current user from SUPUsers table associated with user ID from AspNetUsers table.</returns>
        [Authorize]
        private int getSUPUserID()
        {
            string id = getIdentityID();
            SUPUser supUser = db.SUPUsers.Where(u => u.NetUserId.Equals(id)).FirstOrDefault();
            int supUserid = supUser.Id;
            return supUserid;
        }

        /// <summary>
        /// Retrieves photo to display to view.
        /// </summary>
        /// <param name="id">ID of a photo to display.</param>
        /// <returns>Photo to display to view.</returns>
        // GET: Resources
        public FileResult Photo(int? id)
        {
            SUPImage p = db.SUPImages.Find(id);
            //Stream stream = new MemoryStream(p.Input);
            //Image file = Image.FromStream(stream);
            return File(p.Input, "image");
        }

        public FileResult HomePhoto(int? id)
        {
            SUPImage pid = db.SUPImages.Where(a => a.ItemID == id).FirstOrDefault(); //Finds an image associated with the listing.
            if (pid == null)
            {
                //SUPImage q = new SUPImage();
                //q.Input = Encoding.ASCII.GetBytes("0x89504E470D0A1A0A0000000D49484452000001C2000000FA0403000000C0685A020000001B504C5445CCCCCC9696969C9C9CAAAAAAC5C5C5B7B7B7B1B1B1A3A3A3BEBEBE13580D0D000000097048597300000EC400000EC401952B0E1B000002F949444154789CEDD6BF6FDA4018C6F117071346DB189CD197AAA123A84B4713");
                byte[] q = getNullByteArray();
                return File(q, "image");
            }
            return File(pid.Input, "image");
        }

        public JsonResult NotificationRequest()
        {
            int id = getSUPUserID(); //Retrieve ID of current user.

            var notifications = db.SUPTransactions.Join(db.SUPUsers, t => t.RenterID, u => u.Id, (t, u) => new { t, u })
                                                   .Join(db.SUPItems, x => x.t.ItemID, i => i.Id, (x, i) => new { x.t, x.u, i })
                                                   .Where(x => x.t.OwnerID == id)
                                                   .Select(y => new { y.i.ItemName, y.u.FirstName, y.u.LastName, y.t.StartDate, y.t.EndDate, y.t.TotalPrice, y.t.TimeStamp }).ToList();


            return Json(notifications, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Returns client's current location information in JSON form.
        /// </summary>
        /// <returns>Client's current location info</returns>
        public JsonResult CurrentLocation()
        {
            //URI to contact IPLocate's servers using client's current IP address
            string uri = "https://www.iplocate.io/api/lookup/" + Request.UserHostAddress;

            //Create a web request
            WebRequest dataRequest = WebRequest.Create(uri);

            //Get JSON data 
            Stream dataStream = dataRequest.GetResponse().GetResponseStream();

            //Parse the received JSON data
            var parsedData = new System.Web.Script.Serialization.JavaScriptSerializer()
                                  .DeserializeObject(new StreamReader(dataStream)
                                  .ReadToEnd());

            //return JSON data
            return Json(parsedData, JsonRequestBehavior.AllowGet);
        }

        public byte[] getNullByteArray()
        {
            byte[] b = new byte[878];
            b[0] = 137;
            b[1] = 80;
            b[2] = 78;
            b[3] = 71;
            b[4] = 13;
            b[5] = 10;
            b[6] = 26;
            b[7] = 10;
            b[8] = 0;
            b[9] = 0;
            b[10] = 0;

            b[11] = 13;
            b[12] = 73;
            b[13] = 72;
            b[14] = 68;
            b[15] = 82;
            b[16] = 0;
            b[17] = 0;
            b[18] = 1;
            b[19] = 194;
            b[20] = 0;

            b[21] = 0;
            b[22] = 0;
            b[23] = 250;
            b[24] = 4;
            b[25] = 3;
            b[26] = 0;
            b[27] = 0;
            b[28] = 0;
            b[29] = 192;
            b[30] = 104;

            b[31] = 90;
            b[32] = 2;
            b[33] = 0;
            b[34] = 0;
            b[35] = 0;
            b[36] = 27;
            b[37] = 80;
            b[38] = 76;
            b[39] = 84;
            b[40] = 69;

            b[41] = 204;
            b[42] = 204;
            b[43] = 204;
            b[44] = 150;
            b[45] = 150;
            b[46] = 150;
            b[47] = 156;
            b[48] = 156;
            b[49] = 156;
            b[50] = 170;

            b[51] = 170;
            b[52] = 170;
            b[53] = 197;
            b[54] = 197;
            b[55] = 197;
            b[56] = 183;
            b[57] = 183;
            b[58] = 183;
            b[59] = 177;
            b[60] = 177;

            b[61] = 177;
            b[62] = 163;
            b[63] = 163;
            b[64] = 163;
            b[65] = 190;
            b[66] = 190;
            b[67] = 190;
            b[68] = 19;
            b[69] = 88;
            b[70] = 13;

            b[71] = 13;
            b[72] = 0;
            b[73] = 0;
            b[74] = 0;
            b[75] = 9;
            b[76] = 112;
            b[77] = 72;
            b[78] = 89;
            b[79] = 115;
            b[80] = 0;

            b[81] = 0;
            b[82] = 14;
            b[83] = 196;
            b[84] = 0;
            b[85] = 0;
            b[86] = 14;
            b[87] = 196;
            b[88] = 1;
            b[89] = 149;
            b[90] = 43;

            b[91] = 14;
            b[92] = 27;
            b[93] = 0;
            b[94] = 0;
            b[95] = 2;
            b[96] = 249;
            b[97] = 73;
            b[98] = 68;
            b[99] = 65;
            b[100] = 84;

            b[101] = 120;
            b[102] = 156;
            b[103] = 237;
            b[104] = 214;
            b[105] = 191;
            b[106] = 111;
            b[107] = 218;
            b[108] = 64;
            b[109] = 24;
            b[110] = 198;

            b[111] = 241;
            b[112] = 23;
            b[113] = 7;
            b[114] = 19;
            b[115] = 70;
            b[116] = 219;
            b[117] = 24;
            b[118] = 156;
            b[119] = 209;
            b[120] = 151;

            b[121] = 170;
            b[122] = 161;
            b[123] = 35;
            b[124] = 168;
            b[125] = 75;
            b[126] = 71;
            b[127] = 19;
            b[128] = 85;
            b[129] = 237;
            b[130] = 138;

            b[131] = 167;
            b[132] = 174;
            b[133] = 164;
            b[134] = 75;
            b[135] = 87;
            b[136] = 28;
            b[137] = 53;
            b[138] = 105;
            b[139] = 71;
            b[140] = 232;

            b[141] = 210;
            b[142] = 254;
            b[143] = 217;
            b[144] = 125;
            b[145] = 95;
            b[146] = 187;
            b[147] = 38;
            b[148] = 129;
            b[149] = 144;
            b[150] = 68;

            b[151] = 10;
            b[152] = 16;
            b[153] = 163;
            b[154] = 234;
            b[155] = 251;
            b[156] = 25;
            b[157] = 114;
            b[158] = 63;
            b[159] = 112;
            b[160] = 78;

            b[161] = 247;
            b[162] = 224;
            b[163] = 227;
            b[164] = 238;
            b[165] = 68;
            b[166] = 0;
            b[167] = 0;
            b[168] = 0;
            b[169] = 0;
            b[170] = 0;

            b[171] = 0;
            b[172] = 0;
            b[173] = 0;
            b[174] = 0;
            b[175] = 0;
            b[176] = 0;
            b[177] = 0;
            b[178] = 0;
            b[179] = 0;
            b[180] = 0;

            b[181] = 0;
            b[182] = 0;
            b[183] = 0;
            b[184] = 0;
            b[185] = 0;
            b[186] = 0;
            b[187] = 0;
            b[188] = 0;
            b[189] = 0;
            b[190] = 0;

            b[191] = 0;
            b[192] = 0;
            b[193] = 0;
            b[194] = 0;
            b[195] = 0;
            b[196] = 0;
            b[197] = 0;
            b[198] = 0;
            b[199] = 0;
            b[200] = 0;

            b[201] = 0;
            b[202] = 0;
            b[203] = 0;
            b[204] = 0;
            b[205] = 112;
            b[206] = 120;
            b[207] = 94;
            b[208] = 79;
            b[209] = 255;
            b[210] = 184;

            b[211] = 141;
            b[212] = 206;
            b[213] = 205;
            b[214] = 246;
            b[215] = 54;
            b[216] = 209;
            b[217] = 250;
            b[218] = 48;
            b[219] = 193;
            b[220] = 190;

            b[221] = 38;
            b[222] = 180;
            b[223] = 119;
            b[224] = 94;
            b[225] = 152;
            b[226] = 61;
            b[227] = 153;
            b[228] = 176;
            b[229] = 149;
            b[230] = 174;

            b[231] = 170;
            b[232] = 235;
            b[233] = 153;
            b[234] = 162;
            b[235] = 181;
            b[236] = 255;
            b[237] = 58;
            b[238] = 234;
            b[239] = 132;
            b[240] = 163;

            b[241] = 255;
            b[242] = 61;
            b[243] = 97;
            b[244] = 20;
            b[245] = 191;
            b[246] = 52;
            b[247] = 225;
            b[248] = 159;
            b[249] = 251;
            b[250] = 195;

            b[251] = 28;
            b[252] = 113;
            b[253] = 194;
            b[254] = 126;
            b[255] = 239;
            b[256] = 165;
            b[257] = 9;
            b[258] = 215;
            b[259] = 134;
            b[260] = 57;

            b[261] = 226;
            b[262] = 132;
            b[263] = 129;
            b[264] = 203;
            b[265] = 44;
            b[266] = 145;
            b[267] = 95;
            b[268] = 220;
            b[269] = 84;
            b[270] = 205;

            b[271] = 241;
            b[272] = 133;
            b[273] = 38;
            b[274] = 236;
            b[275] = 184;
            b[276] = 159;
            b[277] = 85;
            b[278] = 151;
            b[279] = 31;
            b[280] = 134;

            b[281] = 245;
            b[282] = 71;
            b[283] = 121;
            b[284] = 24;
            b[285] = 212;
            b[286] = 85;
            b[287] = 233;
            b[288] = 184;
            b[289] = 145;
            b[290] = 173;

            b[291] = 210;
            b[292] = 238;
            b[293] = 117;
            b[294] = 50;
            b[295] = 21;
            b[296] = 119;
            b[297] = 18;
            b[298] = 205;
            b[299] = 44;
            b[300] = 225;

            b[301] = 111;
            b[302] = 247;
            b[303] = 86;
            b[304] = 78;
            b[305] = 206;
            b[306] = 68;
            b[307] = 138;
            b[308] = 172;
            b[309] = 185;
            b[310] = 52;

            b[311] = 219;
            b[312] = 120;
            b[313] = 65;
            b[314] = 62;
            b[315] = 178;
            b[316] = 132;
            b[317] = 87;
            b[318] = 81;
            b[319] = 56;
            b[320] = 179;

            b[321] = 102;
            b[322] = 114;
            b[323] = 81;
            b[324] = 204;
            b[325] = 197;
            b[326] = 141;
            b[327] = 67;
            b[328] = 221;
            b[329] = 128;
            b[330] = 172;

            b[331] = 171;
            b[332] = 91;
            b[333] = 36;
            b[334] = 245;
            b[335] = 71;
            b[336] = 191;
            b[337] = 195;
            b[338] = 121;
            b[339] = 93;
            b[330] = 149;

            b[341] = 34;
            b[342] = 234;
            b[343] = 89;
            b[344] = 194;
            b[345] = 101;
            b[346] = 228;
            b[347] = 250;
            b[348] = 226;
            b[349] = 174;
            b[350] = 191;

            b[351] = 37;
            b[352] = 58;
            b[353] = 76;
            b[354] = 55;
            b[355] = 28;
            b[356] = 134;
            b[357] = 51;
            b[358] = 127;
            b[359] = 176;
            b[360] = 249;

            b[361] = 114;
            b[362] = 155;
            b[363] = 231;
            b[364] = 5;
            b[365] = 94;
            b[366] = 172;
            b[367] = 115;
            b[368] = 109;
            b[369] = 71;
            b[370] = 217;

            b[371] = 101;
            b[372] = 108;
            b[373] = 205;
            b[374] = 196;
            b[375] = 222;
            b[376] = 131;
            b[377] = 59;
            b[378] = 147;
            b[379] = 241;
            b[380] = 168;

            b[381] = 234;
            b[382] = 106;
            b[383] = 165;
            b[384] = 245;
            b[385] = 71;
            b[386] = 58;
            b[387] = 245;
            b[388] = 186;
            b[389] = 218;
            b[390] = 141;

            b[391] = 178;
            b[392] = 165;
            b[393] = 37;
            b[394] = 44;
            b[395] = 22;
            b[396] = 221;
            b[397] = 68;
            b[398] = 220;
            b[399] = 92;
            b[400] = 198;

            b[401] = 11;
            b[402] = 47;
            b[403] = 56;
            b[404] = 233;
            b[405] = 203;
            b[406] = 50;
            b[407] = 109;
            b[408] = 39;
            b[409] = 210;
            b[410] = 29;

            b[411] = 52;
            b[412] = 154;
            b[413] = 231;
            b[414] = 33;
            b[415] = 47;
            b[416] = 208;
            b[417] = 239;
            b[418] = 221;
            b[419] = 137;
            b[420] = 206;

            b[421] = 207;
            b[422] = 183;
            b[423] = 147;
            b[424] = 209;
            b[425] = 235;
            b[426] = 139;
            b[427] = 206;
            b[428] = 210;
            b[429] = 205;
            b[430] = 100;

            b[431] = 50;
            b[432] = 175;
            b[433] = 186;
            b[434] = 90;
            b[435] = 105;
            b[436] = 253;
            b[437] = 145;
            b[438] = 38;
            b[439] = 172;
            b[440] = 171;

            b[441] = 250;
            b[442] = 37;
            b[443] = 116;
            b[444] = 44;
            b[445] = 161;
            b[446] = 190;
            b[447] = 174;
            b[448] = 247;
            b[449] = 226;
            b[450] = 166;

            b[451] = 250;
            b[452] = 176;
            b[453] = 38;
            b[454] = 76;
            b[455] = 109;
            b[456] = 169;
            b[457] = 22;
            b[458] = 89;
            b[459] = 231;
            b[460] = 172;

            b[461] = 209;
            b[462] = 60;
            b[463] = 15;
            b[464] = 233;
            b[465] = 172;
            b[466] = 92;
            b[467] = 230;
            b[468] = 228;
            b[469] = 84;
            b[470] = 119;

            b[471] = 138;
            b[472] = 196;
            b[473] = 154;
            b[474] = 113;
            b[475] = 121;
            b[476] = 14;
            b[477] = 100;
            b[478] = 210;
            b[479] = 74;
            b[480] = 171;

            b[481] = 174;
            b[482] = 186;
            b[483] = 20;
            b[484] = 235;
            b[485] = 95;
            b[486] = 61;
            b[487] = 21;
            b[488] = 232;
            b[489] = 107;
            b[490] = 212;

            b[491] = 132;
            b[492] = 238;
            b[493] = 131;
            b[494] = 54;
            b[495] = 202;
            b[496] = 135;
            b[497] = 203;
            b[498] = 157;
            b[499] = 102;
            b[500] = 25;

            b[501] = 200;
            b[502] = 213;
            b[503] = 244;
            b[504] = 52;
            b[505] = 109;
            b[506] = 42;
            b[507] = 202;
            b[508] = 35;
            b[509] = 116;
            b[510] = 106;

            b[511] = 249;
            b[512] = 204;
            b[513] = 73;
            b[514] = 43;
            b[515] = 21;
            b[516] = 57;
            b[517] = 175;
            b[518] = 154;
            b[519] = 54;
            b[520] = 115;

            b[521] = 219;
            b[522] = 67;
            b[523] = 171;
            b[524] = 174;
            b[525] = 186;
            b[526] = 20;
            b[527] = 75;
            b[528] = 88;
            b[529] = 87;
            b[530] = 181;

            b[531] = 108;
            b[532] = 151;
            b[533] = 191;
            b[534] = 195;
            b[535] = 240;
            b[536] = 77;
            b[537] = 185;
            b[538] = 77;
            b[539] = 233;
            b[540] = 82;

            b[541] = 183;
            b[542] = 157;
            b[543] = 70;
            b[544] = 183;
            b[545] = 162;
            b[546] = 201;
            b[547] = 104;
            b[548] = 50;
            b[549] = 122;
            b[550] = 221;

            b[551] = 0;
            b[552] = 207;
            b[553] = 210;
            b[554] = 169;
            b[555] = 121;
            b[556] = 113;
            b[557] = 149;
            b[558] = 208;
            b[559] = 85;
            b[560] = 77;

            b[561] = 205;
            b[562] = 176;
            b[563] = 74;
            b[564] = 232;
            b[565] = 86;
            b[566] = 165;
            b[567] = 212;
            b[568] = 9;
            b[569] = 203;
            b[570] = 167;

            b[571] = 210;
            b[572] = 234;
            b[573] = 196;
            b[574] = 111;
            b[575] = 127;
            b[576] = 9;
            b[577] = 251;
            b[578] = 101;
            b[579] = 66;
            b[580] = 125;

            b[581] = 135;
            b[582] = 126;
            b[583] = 120;
            b[584] = 251;
            b[585] = 75;
            b[586] = 199;
            b[587] = 74;
            b[588] = 243;
            b[589] = 233;
            b[590] = 43;

            b[591] = 39;
            b[592] = 120;
            b[593] = 142;
            b[594] = 70;
            b[595] = 242;
            b[596] = 7;
            b[597] = 235;
            b[598] = 171;
            b[599] = 52;
            b[600] = 169;

            b[601] = 18;
            b[602] = 62;
            b[603] = 177;
            b[604] = 74;
            b[605] = 181;
            b[606] = 108;
            b[607] = 87;
            b[608] = 119;
            b[609] = 154;
            b[610] = 79;

            b[611] = 213;
            b[612] = 146;
            b[613] = 158;
            b[614] = 151;
            b[615] = 27;
            b[616] = 150;
            b[617] = 142;
            b[618] = 213;
            b[619] = 137;
            b[620] = 199;

            b[621] = 13;
            b[622] = 134;
            b[623] = 217;
            b[624] = 170;
            b[625] = 92;
            b[626] = 150;
            b[627] = 247;
            b[628] = 118;
            b[629] = 154;
            b[630] = 129;

            b[631] = 149;
            b[632] = 101;
            b[633] = 194;
            b[634] = 167;
            b[635] = 119;
            b[636] = 26;
            b[637] = 223;
            b[638] = 18;
            b[639] = 158;
            b[640] = 151;

            b[641] = 75;
            b[642] = 122;
            b[643] = 38;
            b[644] = 249;
            b[645] = 200;
            b[646] = 11;
            b[647] = 90;
            b[648] = 169;
            b[649] = 76;
            b[650] = 244;

            b[651] = 231;
            b[652] = 217;
            b[653] = 31;
            b[654] = 54;
            b[655] = 24;
            b[656] = 102;
            b[657] = 43;
            b[658] = 75;
            b[659] = 152;
            b[660] = 135;

            b[661] = 119;
            b[662] = 167;
            b[663] = 69;
            b[664] = 180;
            b[665] = 184;
            b[666] = 140;
            b[667] = 171;
            b[668] = 132;
            b[669] = 79;
            b[670] = 157;

            b[671] = 22;
            b[672] = 61;
            b[673] = 57;
            b[674] = 45;
            b[675] = 79;
            b[676] = 139;
            b[677] = 169;
            b[678] = 157;
            b[679] = 22;
            b[680] = 239;

            b[681] = 218;
            b[682] = 46;
            b[683] = 179;
            b[684] = 119;
            b[685] = 216;
            b[686] = 46;
            b[687] = 108;
            b[688] = 137;
            b[689] = 247;
            b[690] = 155;

            b[691] = 76;
            b[692] = 179;
            b[693] = 141;
            b[694] = 37;
            b[695] = 244;
            b[696] = 194;
            b[697] = 123;
            b[698] = 39;
            b[699] = 190;
            b[700] = 149;

            b[701] = 101;
            b[702] = 194;
            b[703] = 170;
            b[704] = 203;
            b[705] = 139;
            b[706] = 86;
            b[707] = 199;
            b[708] = 188;
            b[709] = 75;
            b[710] = 87;

            b[711] = 213;
            b[712] = 241;
            b[713] = 237;
            b[714] = 208;
            b[715] = 18;
            b[716] = 230;
            b[717] = 145;
            b[718] = 158;
            b[719] = 156;
            b[720] = 174;

            b[721] = 112;
            b[722] = 3;
            b[723] = 29;
            b[724] = 166;
            b[725] = 19;
            b[726] = 22;
            b[727] = 63;
            b[728] = 162;
            b[729] = 76;
            b[730] = 198;

            b[731] = 113;
            b[732] = 67;
            b[733] = 65;
            b[734] = 30;
            b[735] = 101;
            b[736] = 9;
            b[737] = 253;
            b[738] = 240;
            b[739] = 238;
            b[740] = 214;

            b[741] = 22;
            b[742] = 95;
            b[743] = 223;
            b[744] = 200;
            b[745] = 191;
            b[746] = 132;
            b[747] = 101;
            b[748] = 151;
            b[749] = 174;
            b[750] = 198;

            b[751] = 250;
            b[752] = 170;
            b[753] = 150;
            b[754] = 167;
            b[755] = 119;
            b[756] = 183;
            b[757] = 182;
            b[758] = 226;
            b[759] = 187;
            b[760] = 37;

            b[761] = 244;
            b[762] = 139;
            b[763] = 225;
            b[764] = 66;
            b[765] = 220;
            b[766] = 231;
            b[767] = 225;
            b[768] = 212;
            b[769] = 134;
            b[770] = 249;

            b[771] = 218;
            b[772] = 235;
            b[773] = 158;
            b[774] = 103;
            b[775] = 246;
            b[776] = 208;
            b[777] = 113;
            b[778] = 123;
            b[779] = 193;
            b[780] = 13;

            b[781] = 122;
            b[782] = 253;
            b[783] = 218;
            b[784] = 190;
            b[785] = 156;
            b[786] = 237;
            b[787] = 105;
            b[788] = 38;
            b[789] = 135;
            b[790] = 178;

            b[791] = 115;
            b[792] = 194;
            b[793] = 171;
            b[794] = 108;
            b[795] = 79;
            b[796] = 51;
            b[797] = 57;
            b[798] = 148;
            b[799] = 93;
            b[800] = 19;

            b[801] = 126;
            b[802] = 60;
            b[803] = 182;
            b[804] = 123;
            b[805] = 247;
            b[806] = 3;
            b[807] = 187;
            b[808] = 38;
            b[809] = 116;
            b[810] = 71;

            b[811] = 183;
            b[812] = 149;
            b[813] = 110;
            b[814] = 218;
            b[815] = 57;
            b[816] = 225;
            b[817] = 177;
            b[818] = 221;
            b[819] = 217;
            b[820] = 0;

            b[821] = 0;
            b[822] = 0;
            b[823] = 0;
            b[824] = 0;
            b[825] = 0;
            b[826] = 0;
            b[827] = 0;
            b[828] = 0;
            b[829] = 0;
            b[830] = 0;

            b[831] = 0;
            b[832] = 0;
            b[833] = 0;
            b[834] = 0;
            b[835] = 0;
            b[836] = 0;
            b[837] = 0;
            b[838] = 0;
            b[839] = 0;
            b[840] = 0;

            b[841] = 0;
            b[842] = 0;
            b[843] = 0;
            b[844] = 0;
            b[845] = 0;
            b[846] = 0;
            b[847] = 0;
            b[848] = 0;
            b[849] = 0;
            b[850] = 0;

            b[851] = 0;
            b[852] = 0;
            b[853] = 0;
            b[854] = 0;
            b[855] = 56;
            b[856] = 148;
            b[857] = 191;
            b[858] = 34;
            b[859] = 221;
            b[860] = 92;

            b[861] = 113;
            b[862] = 62;
            b[863] = 166;
            b[864] = 234;
            b[865] = 65;
            b[866] = 0;
            b[867] = 0;
            b[868] = 0;
            b[869] = 0;
            b[870] = 73;

            b[871] = 69;
            b[872] = 78;
            b[873] = 68;
            b[874] = 174;
            b[875] = 66;
            b[876] = 96;
            b[877] = 130;

            return (b);
        }
    }
}