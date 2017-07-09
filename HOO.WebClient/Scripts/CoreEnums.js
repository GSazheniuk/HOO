var EnumAttributeTypes = Object.freeze({
    Resource: 0,
    ResourceCost: 1,
    ResourceFlatChange: 2,
    ResourceMultiplierChange: 3,
    Attribute: 4,
    AttributeFlatBonus: 5,
    AtributeMultiplierBonus: 6,
    EternalRequisite: 7,
    FiniteRequisite: 8,
    EternalRequirement: 9,
    FiniteRequirement: 10,
    NoDirectAccess: 11,
    RaceBonus: 12,
});

var EnumObjectAttribute = Object.freeze({
    RadiationLevel: 0,
    Temperature: 1,
    BasePopulationGrowth: 2,
    BaseProduction: 3,
    BaseFarming: 4,
    BaseResearch: 5,
    StarColor: 6,
    NativeCredits: 7,
    CommandPoints: 8,
    ExploreRating: 9,
    ExpandRating: 10,
    ExploitRating: 11,
    ExterminateRating: 12,
    Capitol: 13,
    BaseCarbonMining: 14,
    BaseSilicateMining: 15,
    BaseMetalMining: 16,
    BaseBasaltMining: 17,
    BaseAmoniaExtraction: 18,
    BaseWaterExtraction: 19,
    BaseCarbonExtraction: 20,
    BaseSilicateExtraction: 21,
    BaseMetalExtraction: 22,
    DummyObject: 23,
    Morale: 24,
    Owner: 25,
});

var StarClasses = ['O', 'B', 'A', 'F', 'G', 'K', 'M'];
var StarColors = ["#3366FF", "#6699FF", "#99CCFF", "#66FFFF", "#FFFF66", "#FFCC00", "#FF9900"];
var StarSizes = ['O', 'I', 'II', 'III', 'IV', 'V', 'VII'];

var PlanetTypes = ['Barren', 'Desert', 'Tundra', 'Arid', 'Swamp', 'Ocean', 'Terran', 'Gaia']
var PlanetSizes = ['Tiny', 'Small', 'Medium', 'Large', 'Huge']

var AsteroidDensities = ['Sparse', 'Occasional', 'Common', 'Rife', 'Dense']
var AsteroidTypes = ['C Type', 'S Type', 'M Type', 'V Type']

var GasGiantSizes = ['Small', 'Normal', 'Large'];
var GasGiantClasses = ['Ammonia Clouds', 'Water Clouds', 'Cloudless', 'Alkali Metals', 'Silicate Clouds'];