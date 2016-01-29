//
//  MakeDictionary.m
//  Eye Of KGB
//
//  Created by bu on 21/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "MakeDictionary.h"

@implementation MakeDictionary

#define JSON_PERSONS @"http://crawler.firstexperience.ru/api/v1/persons/"
#define JSON_SITES @"http://crawler.firstexperience.ru/api/v1/sites/"


//get list of sites from db and put it in array
- (void) getSites {
//    self.sites = [[NSMutableArray alloc] initWithObjects: @"Lenta.ru", @"Gazeta.ru", @"RBC.ru", @"Meduza", @"Fontanka.ru", nil];
    NSArray *data = [[NSArray alloc] init];
    NSData *JSONData = [NSData dataWithContentsOfURL:[NSURL URLWithString:JSON_SITES]];
    NSArray *jsonResult = [NSJSONSerialization JSONObjectWithData:JSONData options:kNilOptions error:nil];
    data = jsonResult;
    NSMutableArray *sitesJSON = [NSMutableArray array];
    for (id item in jsonResult)
        [sitesJSON addObject:[NSString stringWithFormat:@"%@", item[@"sites"]]];
    self.sites = sitesJSON;
}

//get rates from db and put it in array
- (void) getRates {
    self.rates = [[NSMutableArray alloc] initWithObjects: @"10500", @"5000", @"1000", nil];
}

//make dictionary with names and rates (from getRates)
- (void) makeDictionary {
    
    NSArray *data = [[NSArray alloc] init];
    NSData *JSONData = [NSData dataWithContentsOfURL:[NSURL URLWithString:JSON_PERSONS]];
    NSArray *jsonResult = [NSJSONSerialization JSONObjectWithData:JSONData options:kNilOptions error:nil];
    data = jsonResult;
    NSMutableArray *namesJSON = [NSMutableArray array];
    for (id item in jsonResult)
        [namesJSON addObject:[NSString stringWithFormat:@"%@", item[@"persons"]]];
    self.names = namesJSON;
    
}

@end
