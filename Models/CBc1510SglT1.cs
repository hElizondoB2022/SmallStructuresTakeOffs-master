using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallStructuresTakeOffs.Models
{
    public class CBc1510SglT1 : CatchBasin
    {
        public override decimal CBLength { get => 2m + 11.75m/12m; set { decimal L = 2m + 11.75m/12m; } }
        public override decimal CBWidth { get => 2m + 2.5m/12m; set { decimal W = 2m; } }
        public override decimal CBBaseThickness { get => .75m; set { decimal Tb = .75m; } }
        public override decimal CBWallThickness { get => .5m; set { decimal Tw = .5m; } }
        public override int CBVertBars { get => 10; set { decimal Bars = 10m; } }

        public override decimal CBSqRingL { get => (CBLength + CBWidth + 4m * 2m/12m) * 2 + 1m ; set { decimal R = (CBLength + CBWidth + 4m * 2m/12m) * 2 + 1m ; } }

        public override ICollection<CBreinforcement> CBreinforcements
        {
            get => this.theReinforcements(); set => this.theReinforcements();
        }

        public override ICollection<CBreinforcement> theReinforcements()
        {
            IList<CBreinforcement> cbReinf = new List<CBreinforcement>();

            cbReinf.Add(
                    new CBreinforcement
                    {
                        CBId = CatchBasinId,
                        CBRebarNom = RebarNomination.No4,
                        CBreinfCode = "rb01",
                        CBreinfQty = 12,
                        CBreinfLength = CBHeight + CBBaseThickness - (5m/12m) + .5m,
                        CBreinfShape = "L Shape, Vertical, 6\" x L = Length",
                        TotalLength =  (12m) *  (CBHeight + CBBaseThickness - (5m/12m) + .5m),
                        TotalWeight =  (12m) *  (CBHeight + CBBaseThickness - (5m/12m) + .5m) * .668m
                    });
            cbReinf.Add(
                    new CBreinforcement
                    {
                        CBId = CatchBasinId,
                        CBRebarNom = RebarNomination.No4,
                        CBreinfCode = "rb02",
                        CBreinfQty = (int)Math.Ceiling(CBHeight) +1,
                        CBreinfLength = 2m * (CBLength + CBWidth + (5m/12m)) + 2,
                        CBreinfShape = "Square Ring Shape, 2'-Overlap",
                        TotalLength =  ((int)Math.Ceiling(CBHeight) +1) * (2m * (CBLength + CBWidth + (5m/12m)) + 2),
                        TotalWeight =  ((int)Math.Ceiling(CBHeight) +1) * (2m * (CBLength + CBWidth + (5m/12m)) + 2) * .668m
                    });

            return cbReinf;

        }


        //public override ICollection<CBreinforcement> CBreinforcements { set => throw new NotImplementedException(); }

        //public override ICollection<CBreinforcement> CBreinforcements { get => throw new NotImplementedException(); }

        public override decimal PourBottom(decimal CBHeight) {
            if (CBHeight < 8)
            {
                return 
                    (2M * (CBLength + 2M * CBWallThickness + CBWidth) * CBWallThickness * (CBHeight +.5m - 2m) +/*Walls*/
                    CBLength * CBWidth * .75m  /*Base*/) / 27M; /*CY*/
            }
            else
            {
                return
                    (2M * (CBLength + 2M * (CBWallThickness + 2m / 12m) + CBWidth) * (CBWallThickness + 2m / 12m) * (CBHeight +.5m - 2m) +/*Walls*/
                    CBLength * CBWidth * .75m  /*Base*/) / 27M; /*CY*/

                //    ((CBLength + 2M * (8m/12m)) * (CBWidth + 2M * (8m/12m)) * (8m/12m) + /*Base*/
                //2M * ((CBLength + 2M * (8m/12m)) + CBWidth) * (8m/12m) * base.CBHeight /*Walls*/
                //) / 27M; /*CY*/
            }
        }
        public override decimal PourTop()
        {
            return
                (2M * (CBLength + 2M * CBWallThickness + CBWidth) * CBWallThickness * 2m  /*Walls*/ +
                (CBLength + 2m * CBWallThickness) * (2m * CBWallThickness + .5m) * .5m * .5m /*Curb Portion*/ + 
                (CBLength + 2m * CBWallThickness) * 2.5m * 7m/12m /*Pan Portion*/) / 27m; 
        }
        public override decimal PourApron()

        {
            return 0m;
                //((11m * 10m * 2m/12m  -
                //3m * 4M * 2m/12m +
                //(2m * (11m + 10M) + 2m * (4m + 3m)) * (((3m + 9m) * (6m / 2m) )/ 144))) / 27m;

            //return (((2M * 4M + 3M) * 2M + (2M * 4M + 2M - .5M * 2M) * 2M) * (0.25M + 0.5M) * .5M / 2M) / 27M + /* Outside Perfimeter CY*/
            //    ((2M * (4M + 3M) * (0.25M + 0.5M) * .5M / 2M))/ 27M + /* Inside Perfimeter CY*/
            //    ((11M * 10M - 4M * 3M) * 2M / 12M) / 27M; /*Apron*/
        }
        public override decimal PurchConcrete(decimal CBHeight)
        {
            return (PourApron()  + PourTop() + PourBottom(CBHeight)) * 1.25M;
        }
        public override decimal FabForms(decimal CBHeight)
        {
            if (CBHeight < 8)
            {
                return
                    2M * (CBLength + CBWidth + 4M *  CBWallThickness) * (CBHeight +.5m) + /*OutsideWalls*/
                    2M * (CBLength + CBWidth) * (CBHeight +.5m); /*InsideWalls*/
            }
            else { 
                return
                    2M * (CBLength + CBWidth + 4M *  (CBWallThickness + 2m/12m)) * (CBHeight + .5m - 2m) + /*OutsideWalls*/
                    2M * (CBLength + CBWidth) * (CBHeight + .5m - 2m ) + /*InsideWalls*/

                    2M * (CBLength + CBWidth + 4M *  (CBWallThickness)) * (2m) + /*OutsideWalls*/
                    2M * (CBLength + CBWidth) * (2m); /*InsideWalls*/

            }
        }
        public override decimal InstBottomForms(decimal CBHeight)
        {
            if (CBHeight < 8)
            {
                return
                    2M * (CBLength + CBWidth + 4M *  CBWallThickness) * (CBHeight +.5m - 2m) + /*OutsideWalls*/
                    2M * (CBLength + CBWidth) * (CBHeight +.5m - 2m); /*InsideWalls*/
            }else
            {
                return
                    2M * (CBLength + CBWidth + 4M *  (CBWallThickness + 2m/12m)) * (CBHeight +.5m - 2m) + /*OutsideWalls*/
                    2M * (CBLength + CBWidth) * (CBHeight +.5m - 2m); /*InsideWalls*/
            }
        }
        public override decimal InstTopForms()
        {
            return
                2M * (CBLength + CBWidth + 4M *  CBWallThickness ) * 2m + /*OutsideWalls*/
                2M * (CBLength + CBWidth) * 2m; /*InsideWalls*/

        }
        public override decimal RebVertLength(decimal CBHeight)
        {
            return CBHeight + .5m  - (3m/12m + 1.5m/12m);
        }
        public override int RebSqRingEa (decimal CBHeight)
        {
            return (int)(Math.Ceiling((CBHeight + .5m - (3m + 1.5m)/12m) / 1.5m)) + 1;
        }

        public decimal RebNo3Length() { return 2m; }
        public decimal RebNo3Qty() { return 9m; }

        public decimal RebNo4Strth() { return CBLength + 2m * CBWallThickness - 1.5m * 2m / 12m;  }

        public decimal RebNo4StrthEa() { return 1m; }

        public override decimal CBRebarTakeOfflb(decimal CBHeight)
        {
            throw new NotImplementedException();
        }
    }
}
