/**********************************************************************************
**
**  DVDStore Application v1.0
**
**  Copyright 2024
**  Developed by:
**     Ronald Garlit.
**
**  This software was created for educational purposes.
**
**  Use is subject to license terms.
***********************************************************************************
**
**  FileName: FilmsPropertyMapper.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the FilmsPropertyMapper class for the DVDStore web application.
**
**  The FilmsPropertyMapper class provides property mapping for sorting and filtering film data.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-28		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.DAL;
using DVDStore.Web.MVC.Common.Exceptions;
using DVDStore.Web.MVC.Common.PropertyMapping;
using DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Common
{
    public class FilmsPropertyMapper
    {
        private readonly Dictionary<string, PropertyMappingValue> _filmsPropertyMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "Filmid", new PropertyMappingValue(new List<string> { "Filmid" }) },
                { "Title", new PropertyMappingValue(new List<string> { "Title" }) },
                { "Releaseyear", new PropertyMappingValue(new List<string> { "Releaseyear" }) },
                { "Rentalrate", new PropertyMappingValue(new List<string> { "Rentalrate" }) },
                { "Rating", new PropertyMappingValue(new List<string> { "Rating" }) }
            };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public FilmsPropertyMapper()
        {
            _propertyMappings.Add(new PropertyMapping<Film, Film>(_filmsPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new PropertyMappingException($"Cannot find exact property mapping instance for <{typeof(TSource)}, {typeof(TDestination)}>");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();
                int indexOfFirstSpace = trimmedField.IndexOf(' ');
                var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
/*
                      _    _  __  _  _ _____  ___ ___
                      | /\ | |__| |\ |   |   |___ |  \
                      |/  \| |  | | \|   |   |___ |__/

         F O R   C R I M E S   A G A I N S T   T H E   E M P I R E

 ________________________  _________________________  _______________________
|        .......       LS||      .x%%%%%%x.         ||  ,.------;:~~:-.      |
|      ::::::;;::.       ||     ,%%%%%%%%%%%        || /:.\`;;|||;:/;;:\     |
|    .::;::::;::::.      ||    ,%%%'  )'  \%        ||:')\`:\||::/.-_':/)    |
|   .::::::::::::::      ||   ,%x%) __   _ Y        ||`:`\\\ ;'||'.''/,.:\   |
|   ::`_```_```;:::.     ||   :%%% ~=-. <=~|        ||==`;.:`|;::/'/./';;=   |
|   ::=-) :=-`  ::::     ||   :%%::. .:,\  |        ||:-/-%%% | |%%%;;_- _:  |
| `::|  / :     `:::     ||   `;%:`\. `-' .'        ||=// %wm)..(mw%`_ :`:\  |
|   '|  `~'     ;:::     ||    ``x`. -===-;         ||;;--', /88\ -,- :-~~|  |
|    :-:==-.   / :'      ||     / `:`.__.;          ||-;~~::'`~^~:`::`/`-=:) |
|    `. _    .'.d8:      ||  .d8b.  :: ..`.         ||(;':)%%%' `%%%.`:``:)\ |
| _.  |88bood88888._     || d88888b.  '  /8         ||(\ %%%/dV##Vb`%%%%:`-. |
|~  `-+8888888888P  `-. _||d888888888b. ( 8b       /|| |);/( ;~~~~ :)\`;;.``\|
|-'     ~~^^^^~~  `./8 ~ ||~   ~`888888b  `8b     /:|| //\'/,/|;;|:(: |.|\;|\|
|8b /  /  |   \  \  `8   ||  ' ' `888888   `8. _ /:/||/) |(/ | / \|\\`( )- ` |
|P        `          8   ||'      )88888b   8b |):X ||;):):)/.):|/) (`:`\\`-`|
|                    8b  ||   ~ - |888888   `8b/:/:\||;%/ //;/(\`.':| ::`\\;`|
|                    `8  ||       |888888    88\/~~;||;/~( \|./;)|.|):;\. \\-|
|                     8b ||       (888888b   88|  / ||/',:\//) ||`.|| (:\)):%|
|         .           `8 ||\       \888888   8-:   /||,|/;/(%;.||| (|(\:- ; :|
|________/_\___________8_||_\_______\88888_.'___\__/||_%__%:__;_:`_;_:_.\%_`_|

L u k e  S k y w a l k e r      H a n   S o l o          C h e w b a c c a

Self-Proclaimed Jedi Knight     Smuggler, Pirate         Smuggler, Pirate
     500,000 credits            200,000 credits          100,000 credits

               The above are wanted for the following crimes:

    - Liberation of a known criminal, Princess Leia Organa of Alderaan -
         - Direct involvement in armed revolt against the Empire -
                              - High treason -
                               - Espionage -
                               - Conspiracy -
                    - Destruction of Imperial Property -

           These individuals are considered extremely dangerous.

       E X P E R I E N C E D   B O U N T Y   H U N T E R S   O N L Y

  The Empire will not  be held  responsible  for any  injuries or property
  loss arising from the  attempted apprehension of these  notorious crimi-
  nals. Bounty is for live capture only! For more information contact your
  local Imperial Intelligence Office.
*/