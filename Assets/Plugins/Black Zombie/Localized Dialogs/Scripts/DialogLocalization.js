////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// 	DialogLocalization.js
//
//	Global Localization Script that handles builds, saves, etc.
//
//	© 2012-2013 Melli Georgiou
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#pragma strict

// Current Language
static var language : String = "English";

// Set Template
var buildTemplate : boolean = false;								// Should we setup the whole app this way?
var languages : LanguageEnabler;									// What Languages will we support in this build?
var debugLanguage : DebugLanguage = DebugLanguage.AutoDetect; 		// Debugs Languages in the Editor only.
		
	// Languages
	class LanguageEnabler {
		var chinese : boolean = false;
		var english : boolean = true;
		var french : boolean = false;
		var german : boolean = false;
		var italian : boolean = false;
		var japanese : boolean = false;
		var korean : boolean = false;
		var portuguese : boolean = false;
		var russian : boolean = false;	
		var spanish : boolean = false;
	}
	
	// DebugLanguages
	enum DebugLanguage {AutoDetect,English,Chinese,Korean,Japanese,Spanish,Italian,German,French,Portuguese,Russian}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	AWAKE
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function Awake() {

	// -----------------------------------------------------------------
	// 	SAVE BUILD TEMPLATE
	//	Saves the Localization Template to Player Prefs
	// -----------------------------------------------------------------
	
	// Save Build Template Settings
	if( buildTemplate ){
		
		// Save Localization Settings
    	SaveLanguages();
    
    // -----------------------------------------------------------------
	// 	LOAD BUILD TEMPLATE
	//	Load Localization Template Settings	
	// -----------------------------------------------------------------
	} else {
		
		// Load Localization Settings
    	LoadLanguages();	
	}
	
	
	// -----------------------------------------------------------------
	// 	FINAL SETUP
	//	Final things to setup ( doesn't matter if this is a build
	//	template or not, this section runs anyway!)
	// -----------------------------------------------------------------
	
	// Final Localization Setup
	Localize();
	//Debug.Log("LDC: (DialogLocalization) Current Language = " + DialogLocalization.language);
	
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	LOCALIZATION SETTINGS
//	Figures out what Language we should use for localization
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function Localize(){
		
	 // Set language using the Unity API. 
	 DialogLocalization.language = Application.systemLanguage.ToString();
	 
	 // Allow for debugging Languages in the editor
	 if ( Application.isEditor ){
	 	if ( debugLanguage != DebugLanguage.AutoDetect ) {
			DialogLocalization.language = debugLanguage.ToString();
	 	} else {
	 		DialogLocalization.language = "English";
	 	}
	 }
	 
	// Convert the language into something we can use ..
	DialogLocalization.language = LanguageCodeToString(DialogLocalization.language);

}


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	LANGUAGE CODE TO STRING
//	Converts Abbreviated language codes into a universal format between platforms
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function LanguageCodeToString( code : String ) {
	
	// Debug
	if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Converting Language Code: "+code); }
	
	// ENGLISH
	if ( 	code.ToLower() == "en" ||  
			code.ToLower() == "eng" ||  
			code.ToLower() == "english") {
		
		// English is ALWAYS valid.
		return "English";
	}
	
	// CHINESE
	else if ( 	code.ToLower() == "zh" ||
				code.ToLower() == "zho" ||  
				code.ToLower() == "chi" || 
				code.ToLower() == "zh-hans" || 
				code.ToLower() == "zho +" ||
				code.ToLower() == "zho+" ||
				code.ToLower() == "chinese" ) {
		
		// Make sure this language is valid
		if( languages.chinese ){
			return "Chinese";
		} else {
			Debug.Log(code.ToLower() + " localization not supported, reverting to English");
			return "English";
		}
	}
	
	// KOREAN
	else if ( 	code.ToLower() == "ko" ||
				code.ToLower() == "kor" ||  
				code.ToLower() == "kur" || 
				code.ToLower() == "kua" ||
				code.ToLower() == "korean" ) {
		
		// Make sure this language is valid
		if( languages.korean ){
			return "Korean";
		} else {
			Debug.Log(code.ToLower() + " localization not supported, reverting to English");
			return "English";
		}
	}
	
	// JAPANESE
	else if ( 	code.ToLower() == "ja" ||
				code.ToLower() == "jpn" ||  
				code.ToLower() == "japanese" ) {
		
		// Make sure this language is valid
		if( languages.japanese ){
			return "Japanese";
		} else {
			Debug.Log(code.ToLower() + " localization not supported, reverting to English");
			return "English";
		}
	}
	
	// GERMAN
	else if ( 	code.ToLower() == "gsw" ||
				code.ToLower() == "de" ||  
				code.ToLower() == "deu" ||  
				code.ToLower() == "ger" || 
				code.ToLower() == "german" ) {
		
		// Make sure this language is valid
		if( languages.german ){
			return "German";
		} else {
			Debug.Log(code.ToLower() + " localization not supported, reverting to English");
			return "English";
		}
	}
	
	// FRENCH
	else if ( 	code.ToLower() == "fr" ||
				code.ToLower() == "fra" ||  
				code.ToLower() == "fre" ||  
				code.ToLower() == "french" ) {
		
		// Make sure this language is valid
		if( languages.french ){
			return "French";
		} else {
			Debug.Log(code.ToLower() + " localization not supported, reverting to English");
			return "English";
		}	
	}
	
	// SPANISH
	else if ( 	code.ToLower() == "spa" ||
				code.ToLower() == "es" || 
				code.ToLower() == "spanish" ) {
		
		// Make sure this language is valid
		if( languages.spanish ){
			return "Spanish";
		} else {
			Debug.Log(code.ToLower() + " localization not supported, reverting to English");
			return "English";
		}	
	}
	
	// ITALIAN
	else if ( 	code.ToLower() == "ita" ||
				code.ToLower() == "it" || 
				code.ToLower() == "italian" ) {
		
		// Make sure this language is valid
		if( languages.italian ){
			return "Italian";
		} else {
			Debug.Log(code.ToLower() + " localization not supported, reverting to English");
			return "English";
		}	
	}
	
	// PORTUGUESE
	else if ( 	code.ToLower() == "pt" ||
				code.ToLower() == "portuguese" ) {
		
		// Make sure this language is valid
		if( languages.italian ){
			return "Portuguese";
		} else {
			Debug.Log(code.ToLower() + " localization not supported, reverting to English");
			return "English";
		}	
	}
	
	// PORTUGUESE
	else if ( 	code.ToLower() == "ru" ||
				code.ToLower() == "russian" ) {
		
		// Make sure this language is valid
		if( languages.italian ){
			return "Russian";
		} else {
			Debug.Log(code.ToLower() + " localization not supported, reverting to English");
			return "English";
		}	
	}
	
	// Default To English
	else {
		
		return "English";	
	}
	
	
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	LANGUAGE LOCALIZATION TEMPLATE - SAVE
//	Used to save data to the PlayerPrefs file from the Build Template
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function SaveLanguages(){
	
	// We only show these console messages on the device.
	if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - SaveLanguages() - Configuring Localizations.."); }
	
	// DELETE OLD KEYS
	if ( PlayerPrefs.HasKey("LANGUAGES_ENGLISH") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_ENGLISH");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for English"); }
    }
    if ( PlayerPrefs.HasKey("LANGUAGES_SPANISH") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_SPANISH");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for Spanish"); }
    }
    if ( PlayerPrefs.HasKey("LANGUAGES_ITALIAN") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_ITALIAN");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for Italian"); }
    }
    if ( PlayerPrefs.HasKey("LANGUAGES_GERMAN") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_GERMAN");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for German"); }
    }
    if ( PlayerPrefs.HasKey("LANGUAGES_FRENCH") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_FRENCH");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for French"); }
    }
    if ( PlayerPrefs.HasKey("LANGUAGES_CHINESE") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_CHINESE");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for Chinese"); }
    }
    if ( PlayerPrefs.HasKey("LANGUAGES_KOREAN") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_KOREAN");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for Korean"); }
    }
    if ( PlayerPrefs.HasKey("LANGUAGES_JAPANESE") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_JAPANESE");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for Japanese"); }
    }
    if ( PlayerPrefs.HasKey("LANGUAGES_PORTUGUESE") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_PORTUGUESE");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for Portuguese"); }
    }
    if ( PlayerPrefs.HasKey("LANGUAGES_RUSSIAN") ) {
    	PlayerPrefs.DeleteKey("LANGUAGES_RUSSIAN");
		if(!Application.isEditor){ Debug.Log("Deleting Localization Entry for Russian"); }
    }
	
	// ----------------------------------------------------------
	
	// ENGLISH
	if ( languages.english ) {
		SaveBool("LANGUAGES_ENGLISH", languages.english);
	}
	
	// SPANISH
	if ( languages.spanish ) {
		SaveBool("LANGUAGES_SPANISH", languages.spanish);
	}
	
	// ITALIAN
	if ( languages.italian ) {
		SaveBool("LANGUAGES_ITALIAN", languages.italian);
	}
	
	// GERMAN
	if ( languages.german ) {
		SaveBool("LANGUAGES_GERMAN", languages.german);
	}
	
	// FRENCH
	if ( languages.french ) {
		SaveBool("LANGUAGES_FRENCH", languages.french);
	}
	
	// CHINESE
	if ( languages.chinese ) {
		SaveBool("LANGUAGES_CHINESE", languages.chinese);
	}
	
	// KOREAN
	if ( languages.korean ) {
		SaveBool("LANGUAGES_KOREAN", languages.korean);
	}
	
	// JAPANESE
	if ( languages.japanese ) {
		SaveBool("LANGUAGES_JAPANESE", languages.japanese);
	}
	
	// PORTUGUESE
	if ( languages.portuguese ) {
		SaveBool("LANGUAGES_PORTUGUESE", languages.portuguese );
	}
	
	// RUSSIAN
	if ( languages.russian ) {
		SaveBool("LANGUAGES_PORTUGUESE", languages.russian );
	}
	
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	LANGUAGE LOCALIZATION TEMPLATE - LOAD
//	Used to load data to the PlayerPrefs file from the Build Template
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function LoadLanguages(){
	
	// ENGLISH
	if ( PlayerPrefs.HasKey("LANGUAGES_ENGLISH") ) {
		languages.english = LoadBool("LANGUAGES_ENGLISH");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: English Enabled: "+languages.english); }	
		}
	} else {
		languages.english = false;
	}
	
	// SPANISH
	if ( PlayerPrefs.HasKey("LANGUAGES_SPANISH") ) {
		languages.spanish = LoadBool("LANGUAGES_SPANISH");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: Spanish Enabled: "+languages.spanish); }	
		}
	} else {
		languages.spanish = false;
	}
	
	// ITALIAN
	if ( PlayerPrefs.HasKey("LANGUAGES_ITALIAN") ) {
		languages.spanish = LoadBool("LANGUAGES_ITALIAN");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: Italian Enabled: "+languages.italian); }	
		}
	} else {
		languages.italian = false;
	}
	
	// GERMAN
	if ( PlayerPrefs.HasKey("LANGUAGES_GERMAN") ) {
		languages.german = LoadBool("LANGUAGES_GERMAN");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: German Enabled: "+languages.german); }
		}
	} else {
		languages.german = false;
	}
	
	// FRENCH
	if ( PlayerPrefs.HasKey("LANGUAGES_FRENCH") ) {
		languages.french = LoadBool("LANGUAGES_FRENCH");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: French Enabled: "+languages.french);	}
		}
	} else {
		languages.french = false;
	}
	
	// CHINESE
	if ( PlayerPrefs.HasKey("LANGUAGES_CHINESE") ) {
		languages.chinese = LoadBool("LANGUAGES_CHINESE");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: Chinese Enabled: "+languages.chinese); }	
		}
	} else {
		languages.chinese = false;
	}
	
	// KOREAN
	if ( PlayerPrefs.HasKey("LANGUAGES_KOREAN") ) {
		languages.korean = LoadBool("LANGUAGES_KOREAN");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: Korean Enabled: "+languages.korean);	}	
		}
	} else {
		languages.korean = false;
	}
	
	// JAPANESE
	if ( PlayerPrefs.HasKey("LANGUAGES_JAPANESE") ) {
		languages.japanese = LoadBool("LANGUAGES_JAPANESE");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: Japanese Enabled: "+languages.japanese);	}
		}
	} else {
		languages.japanese = false;
	}
	
	// PORTUGUESE
	if ( PlayerPrefs.HasKey("LANGUAGES_PORTUGUESE") ) {
		languages.portuguese = LoadBool("LANGUAGES_PORTUGUESE");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: Portuguese Enabled: "+languages.portuguese);	}
		}
	} else {
		languages.portuguese = false;
	}
	
	// RUSSIAN
	if ( PlayerPrefs.HasKey("LANGUAGES_RUSSIAN") ) {
		languages.russian = LoadBool("LANGUAGES_RUSSIAN");
		
		// Show information on the devices
		if(!Application.isEditor){
			if(!Application.isEditor){ Debug.Log("LDC (DialogLocalization) - Localization: Russian Enabled: "+ languages.russian );	}
		}
	} else {
		languages.russian = false;
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	LOCALIZATION SAVE / LOAD
//	Save Routines that handle duplicate entries in the PlayerPrefs file
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	

// SAVE BOOLEAN
static function SaveBool (name : String, value : boolean) {
    PlayerPrefs.SetInt(name, value?1:0);
}

// LOAD BOOLEAN
static function LoadBool (name : String) : boolean {
    return PlayerPrefs.GetInt(name)==1?true:false;
}