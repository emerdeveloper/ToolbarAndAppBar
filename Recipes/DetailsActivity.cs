using System;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Recipes
{
	[Activity(Label = "DetailsActivity")]
	public class DetailsActivity : Android.Support.V7.App.AppCompatActivity
	{
        // Field declaration
        Android.Support.V7.Widget.Toolbar toolbar;
        Recipe recipe;
		ArrayAdapter adapter;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Details);
            //During Activity creation, call SetSupportActionBar to install your Toolbar as your Activity's app bar
            base.SetSupportActionBar(toolbar);

            // Element lookup
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //toolbar.SetLogo(Resource.Drawable.ic_local_dining_white_24dp);
			//
			// Retrieve the recipe to be displayed on this page
			//
			int index = Intent.GetIntExtra("RecipeIndex", -1);
			recipe = RecipeData.Recipes[index];
            toolbar.Title = recipe.Name;// Show the recipe name
            //inflate a toolbar with elements create in XML file
            //toolbar.InflateMenu(Resource.Menu.actions);
            //Subscribe to the toolbar's MenuItemClick event.
            //toolbar.MenuItemClick += OnMenuItemClick;

            //
            // Show the recipe name
            //
            //var name = FindViewById<TextView>(Resource.Id.nameTextView);
            //name.Text = recipe.Name;

            //
            // Show the list of ingredients
            //
            var list = FindViewById<ListView>(Resource.Id.ingredientsListView);
			list.Adapter = adapter = new ArrayAdapter<Ingredient>(this, Android.Resource.Layout.SimpleListItem1, recipe.Ingredients);

			//
			// Set up the "Favorite" toggle, we use different images for the 'on' and 'off' states
			//
			//var toggle = FindViewById<ToggleButton>(Resource.Id.favoriteButton);
			//toggle.CheckedChange += OnFavoriteCheckedChange;
		    //SetFavoriteDrawable(recipe.IsFavorite);

			//
			// Set up the "Number of servings" buttons
			//
			/*FindViewById<Button>(Resource.Id.oneServingButton).Click   += (sender, e) => SetServings(1);
			FindViewById<Button>(Resource.Id.twoServingsButton).Click  += (sender, e) => SetServings(2);
			FindViewById<Button>(Resource.Id.fourServingsButton).Click += (sender, e) => SetServings(4);*/

			//
			// Navigation button: navigate back to the previous page
			//
			FindViewById<ImageButton>(Resource.Id.backButton).Click += (sender, e) => Finish();

			//
			// Navigation button: navigate forward to the About page
			//
			//FindViewById<Button>(Resource.Id.aboutButton).Click += (sender, e) => StartActivity(typeof(AboutActivity));
		}
        //  Override OnCreateOptionsMenu to populate your Toolbar's actions
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.MenuInflater.Inflate(Resource.Menu.actions, menu);
            SetFavoriteDrawable(recipe.IsFavorite);
            return true;
        }

        //Override OnOptionsItemSelected to respond to app bar item click
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)//Identifies which item the user selected
            {
                case Resource.Id.addToFavorites:
                    recipe.IsFavorite = !recipe.IsFavorite;
                    SetFavoriteDrawable(recipe.IsFavorite);
                    break;
                case Resource.Id.about:
                    StartActivity(typeof(AboutActivity));
                    break;
                case Resource.Id.oneServing:
                    SetServings(1);
                    item.SetCheckable(true);
                    break;
                case Resource.Id.twoServings:
                    SetServings(2);
                    item.SetCheckable(true);
                    break;
                case Resource.Id.fourServings:
                    SetServings(4);
                    item.SetCheckable(true);
                    break;
            }
            return true;
        }

        #region Methods
        /*private void OnMenuItemClick(object sender, Android.Support.V7.Widget.Toolbar.MenuItemClickEventArgs e)
        {//switch statement that tests the value of the parameter e.Item.ItemId. This will be the id of the menu items defined in the XML file.
            switch (e.Item.ItemId) {
                case Resource.Id.addToFavorites:
                    recipe.IsFavorite = !recipe.IsFavorite;
                    SetFavoriteDrawable(recipe.IsFavorite);
                    break;
                case Resource.Id.about:
                    StartActivity(typeof(AboutActivity));
                    break;
                case Resource.Id.oneServing:
                    SetServings(1);
                    e.Item.SetCheckable(true);
                    break;
                case Resource.Id.twoServings:
                    SetServings(2);
                    e.Item.SetCheckable(true);
                    break;
                case Resource.Id.fourServings:
                    SetServings(4);
                    e.Item.SetCheckable(true);
                    break;
            }
        }*/

        //
        // Handler for the 'favorite' toggle button
        //
        /*void OnFavoriteCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            recipe.IsFavorite = e.IsChecked; // update the recipe's state

            SetFavoriteDrawable(e.IsChecked); // toggle the image used on the button
        }*/

        void SetFavoriteDrawable(bool isFavorite)
        {

            if (isFavorite)
            {
                toolbar.Menu.FindItem(Resource.Id.addToFavorites).SetIcon(Resource.Drawable.ic_favorite_white_24dp);
            }
            else {
                toolbar.Menu.FindItem(Resource.Id.addToFavorites).SetIcon(Resource.Drawable.ic_favorite_border_white_24dp);
            }

            /*Drawable drawable = null;

            if (isFavorite)
                drawable = base.GetDrawable(Resource.Drawable.ic_favorite_white_24dp); // filled in 'heart' image
            else
                drawable = base.GetDrawable(Resource.Drawable.ic_favorite_border_white_24dp); // 'heart' image border only

            FindViewById<ToggleButton>(Resource.Id.favoriteButton).SetCompoundDrawablesWithIntrinsicBounds(null, drawable, null, null);*/
        }
        // Note: base.GetDrawable requires API level 21
        // To run on earlier versions, change the minimum API level in the project settings and use the following code:
        //void SetFavoriteDrawables(bool isFavorite)
        //{
        //	Drawable drawable = null;
        //
        //	if (isFavorite)
        //		drawable = Resources.GetDrawable(Resource.Drawable.ic_favorite_white_24dp);
        //	else
        //		drawable = Resources.GetDrawable(Resource.Drawable.ic_favorite_border_white_24dp);
        //
        //	FindViewById<ToggleButton>(Resource.Id.favoriteButton).SetCompoundDrawablesWithIntrinsicBounds(null, drawable, null, null);
        //}

        void SetServings(int numServings)
        {
            recipe.NumServings = numServings;

            adapter.NotifyDataSetChanged();
        } 

        #endregion
    }
}