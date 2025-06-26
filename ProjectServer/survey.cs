
using System.Text.Json.Serialization;


public record class PassengerSurveyObject(
    [property: JsonPropertyName("odpt:surveyYear")] int SurveyYear,
    [property: JsonPropertyName("odpt:passengerJourneys")] int PassengerJourneys
);

public record class Survey(
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("odpt:operator")] string Operator,
    [property: JsonPropertyName("odpt:railway")] string[] Railway,
    [property: JsonPropertyName("odpt:station")] string[] Station,
    [property: JsonPropertyName("odpt:includeAlighting")] bool IncludeAlighting,
    [property: JsonPropertyName("odpt:passengerSurveyObject")] PassengerSurveyObject[] PassengerSurveyObject
);

/////////////////////////////////////////////////////////

public record class Titles(
    [property: JsonPropertyName("ja")] string Ja,
    [property: JsonPropertyName("en")] string En

    );

public record class RailwayDirection(
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("dc:title")] string Title,
    [property: JsonPropertyName("odpt:railDirectionTitle")] Titles RailDirectionTitle
    );

/////////////////////////////////////////////////////////



public record class stationOrder(
    [property: JsonPropertyName("odpt:index")] int Index,
    [property: JsonPropertyName("odpt:station")] string Station,
    [property: JsonPropertyName("odpt:stationTitle")] Titles StationTitle
    );

public record class Railway(
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("dc:title")] string Title,
    [property: JsonPropertyName("odpt:railwayTitle")] Titles RailwayTitle,
    [property: JsonPropertyName("odpt:operator")] string Operator,
    [property: JsonPropertyName("odpt:ascendingRailDirection")] string AscendingRailDirection,
    [property: JsonPropertyName("odpt:descendingRailDirection")] string DescendingRailDirection,
    [property: JsonPropertyName("odpt:stationOrder")] stationOrder[] StationOrder


    );
/////////////////////////////////////////////////////////


public record class RailwayFare(
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("odpt:operator")] string Operator,
    [property: JsonPropertyName("odpt:fromStation")] string FromStation,
    [property: JsonPropertyName("odpt:toStation")] string ToStation,
    [property: JsonPropertyName("odpt:ticketFare")] int TicketFare,
    [property: JsonPropertyName("odpt:icCardFare")] int IcCardFare,
    [property: JsonPropertyName("odpt:childTicketFare")] int ChildTicketFare


    );

/////////////////////////////////////////////////////////

public record class Station(
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("dc:title")] string Title,
    [property: JsonPropertyName("odpt:stationTitle")] Titles StationTitle,
    [property: JsonPropertyName("odpt:operator")] string Operator,
    [property: JsonPropertyName("odpt:railway")] string Railway,
    [property: JsonPropertyName("geo:long")] float Long,
    [property: JsonPropertyName("geo:lat")] float Lat

    );
/////////////////////////////////////////////////////////
public record class stationTimetableObject(
    [property: JsonPropertyName("odpt:departureTime")] string DepartureTime,
    [property: JsonPropertyName("odpt:destinationStation")] string[] DestinationStation,
    [property: JsonPropertyName("odpt:trainType")] string TrainType


    );

public record class StationTimetable(
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("dct:issued")] string Issued,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("odpt:operator")] string Operator,
    [property: JsonPropertyName("odpt:railway")] string Railway,
    [property: JsonPropertyName("odpt:station")] string Station,
    [property: JsonPropertyName("odpt:railDirection")] string RailDirection,
    [property: JsonPropertyName("odpt:calendar")] string Calendar,
    [property: JsonPropertyName("odpt:stationTimetableObject")] stationTimetableObject[] StationTimetableObject
    );

/////////////////////////////////////////////////////////
//does not work
public record class Train(
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("odpt:operator")] string Operator,
    [property: JsonPropertyName("odpt:railway")] string Railway,
    [property: JsonPropertyName("odpt:railDirection")] string RailDirection,
    [property: JsonPropertyName("odpt:trainNumber")] string TrainNumber,
    [property: JsonPropertyName("odpt:fromStation")] string FromStation,
    [property: JsonPropertyName("odpt:toStation")] string ToStation,
    [property: JsonPropertyName("odpt:destinationStation")] string[] DestinationStation,
    [property: JsonPropertyName("odpt:index")] string Index,
    [property: JsonPropertyName("odpt:delay")] string Delay,
    [property: JsonPropertyName("odpt:carComposition")] string CarComposition

    );


/////////////////////////////////////////////////////////
public record class TrainInformation(
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("dct:valid")] string Valid,
    [property: JsonPropertyName("odpt:timeOfOrigin")] string TimeOfOrigin,
    [property: JsonPropertyName("odpt:operator")] string Operator,
    [property: JsonPropertyName("odpt:railway")] string Railway,
    [property: JsonPropertyName("odpt:trainInformationText")] Titles TrainInformationText

    );
/////////////////////////////////////////////////////////
public record class trainTimeTableObject(
    [property: JsonPropertyName("odpt:departureTime")] string DepartureTime,
    [property: JsonPropertyName("odpt:departureStation")] string DepartureStation
    );

public record class TrainTimetable(
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("dct:issued")] string Issued,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("odpt:operator")] string Operator,
    [property: JsonPropertyName("odpt:railway")] string Railway,
    [property: JsonPropertyName("odpt:railDirection")] string RailDirection,
    [property: JsonPropertyName("odpt:calendar")] string Calendar,
    [property: JsonPropertyName("odpt:trainNumber")] string TrainNumber,
    [property: JsonPropertyName("odpt:trainType")] string TrainType,
    [property: JsonPropertyName("odpt:originStation")] string[] OriginStation,
    [property: JsonPropertyName("odpt:destinationStation")] string[] DestinationStation,
    [property: JsonPropertyName("odpt:trainTimetableObject")] trainTimeTableObject[] TrainTimetableObject
    );

/////////////////////////////////////////////////////////
public record class TrainType(
    [property: JsonPropertyName("@context")] string Context,
    [property: JsonPropertyName("@id")] string Id,
    [property: JsonPropertyName("@type")] string Type,
    [property: JsonPropertyName("dc:date")] string Date,
    [property: JsonPropertyName("owl:sameAs")] string SameAs,
    [property: JsonPropertyName("odpt:operator")] string Operator,
    [property: JsonPropertyName("odpt:title")] string Title,
    [property: JsonPropertyName("odpt:trainTypeTitle")] Titles TrainTypeTitle

    );


///////////////////////////////////////////////////////////////

public class translationitem
{
    public string text { get; set; }
    public string to { get; set; }

}

public class translation
{

    public List<translationitem> translations { get; set; }
}