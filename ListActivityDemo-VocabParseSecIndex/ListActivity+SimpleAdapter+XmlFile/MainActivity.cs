﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System.Collections.Generic;
using Android.Runtime;
using System;

namespace ListActivitySimpleAdapterXmlFile
{
	[Activity (Label = "Spanish-English Vocabulary", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : ListActivity
	{
        // Overriding OnCreate in the Activity super-class
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Read and prase the vocabulary file and provide the adapter with the data
			var reader = new XmlVocabFileParser(Assets.Open(@"spanish-english.xml"));
			var dataList = reader.VocabList;

			// Sort the List of Dictionary objects. Sort primarily on part of speech, secondarily on Spanish
			// To do this we must provide a comparator for List.Sort
			// Note that the List must be sorted in order for the section indexer to work properly
			dataList.Sort((x, y) => String.Compare((string)x[XmlVocabFileParser.POS] + (string)x[XmlVocabFileParser.SPANISH], 
			      	(string)y[XmlVocabFileParser.POS] + (string)y[XmlVocabFileParser.SPANISH],	  		
					StringComparison.Ordinal));      

            // constructor takes: reference to this Activity, List of Dictionary objects, row layout, 
			ListAdapter = new VocabAdapter (this, dataList,
				Android.Resource.Layout.SimpleListItem1,
				new string[] {XmlVocabFileParser.SPANISH},
				new int[] {Android.Resource.Id.Text1}
			);

			// This is all you need to do to enable fast scrolling
			ListView.FastScrollEnabled = true;

		}

		// Overriding an event handler in the ListActivity super-class
		protected override void OnListItemClick(ListView l,
			View v, int position, long id)
		{
            // Get the English word and part of speech for the spanish word that was clicked on
			string word = (string)((JavaDictionary<string,object>)ListView.GetItemAtPosition(position))[XmlVocabFileParser.ENGLISH];
			string pos = (string)((JavaDictionary<string,object>)ListView.GetItemAtPosition(position))[XmlVocabFileParser.POS];
			Android.Widget.Toast.MakeText(this,
				word + ", " + pos,
				Android.Widget.ToastLength.Short).Show();
		}

	}



}


