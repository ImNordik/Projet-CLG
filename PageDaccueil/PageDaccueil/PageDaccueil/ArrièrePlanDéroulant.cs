using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace PageDaccueil
{
   /// <summary>
   /// This is a game component that implements IUpdateable.
   /// </summary>
   public class ArrièrePlanDéroulant : Microsoft.Xna.Framework.DrawableGameComponent
   {
      SpriteBatch GestionSprites { get; set; }
      Texture2D ImageDeFond { get; set; }
      string NomImage { get; set; }
      float IntervalleMAJ { get; set; }
      Rectangle ZoneAffichage { get; set; }
      float TempsÉcouléDepuisTranslation { get; set; }

      public ArrièrePlanDéroulant(Game jeu, string nomImage, float intervalleMAJ)
         : base(jeu)
      {
         NomImage = nomImage;
         IntervalleMAJ = intervalleMAJ;
      }
      public override void Initialize()
      {
         ZoneAffichage = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
         TempsÉcouléDepuisTranslation = 0;
         base.Initialize();
      }

      protected override void LoadContent()
      {
         GestionSprites = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch; 
         ImageDeFond = Game.Content.Load<Texture2D>("Textures/" + NomImage);
         base.LoadContent();
      }
      public override void Update(GameTime gameTime)
      {
         float tempsÉcoulé = (float)gameTime.ElapsedGameTime.TotalSeconds;
         TempsÉcouléDepuisTranslation += tempsÉcoulé;

         if (TempsÉcouléDepuisTranslation >= IntervalleMAJ)
         {
            if(ZoneAffichage.X > Game.Window.ClientBounds.Width)
            {
               ZoneAffichage = new Rectangle(0, 0, ZoneAffichage.Width, ZoneAffichage.Height);
            }
            ZoneAffichage = new Rectangle(ZoneAffichage.X + 1, 0,ZoneAffichage.Width,ZoneAffichage.Height);
            TempsÉcouléDepuisTranslation = 0;
         }
         base.Update(gameTime);
      }
      public override void Draw(GameTime gameTime)
      {
         GestionSprites.Draw(ImageDeFond, ZoneAffichage, Color.White);
         GestionSprites.Draw(ImageDeFond,new Rectangle(ZoneAffichage.X- Game.Window.ClientBounds.Width, 0,ZoneAffichage.Width,ZoneAffichage.Height), Color.White);
         base.Draw(gameTime);
      }
   }
}
