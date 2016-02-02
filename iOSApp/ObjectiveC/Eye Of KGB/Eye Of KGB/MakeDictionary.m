//
//  MakeDictionary.m
//  Eye Of KGB
//
//  Created by bu on 21/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "MakeDictionary.h"

@interface MakeDictionary ()


@end


@implementation MakeDictionary

#define JSON_PERSONS @"http://crawler.firstexperience.ru/api/v1/persons/"
#define JSON_SITES @"http://crawler.firstexperience.ru/api/v1/sites/"


//get list of sites from db and put it in array
- (void) getSites {
    
    NSArray *data = [[NSArray alloc] init];
    NSData *JSONData = [NSData dataWithContentsOfURL:[NSURL URLWithString:JSON_SITES]];
    NSArray *jsonResult = [NSJSONSerialization JSONObjectWithData:JSONData options:kNilOptions error:nil];
    data = jsonResult;
    NSMutableArray *sitesJSON = [NSMutableArray array];
    NSMutableArray *sitesIDJSON = [NSMutableArray array];

    for (id item in jsonResult) {
        [sitesJSON addObject:[NSString stringWithFormat:@"%@", item[@"sites"]]];
        [sitesIDJSON addObject:[NSString stringWithFormat:@"%@", item[@"id"]]];
    }
    self.sites = sitesJSON;
    self.sitesID = sitesIDJSON;
    NSLog(@"sitesID = %@",self.sitesID);
}

//get rates from db and put it in array
- (void) getRates {
//    self.rates = [[NSMutableArray alloc] initWithObjects: @"10500", @"5000", @"1000", nil];
    
//    NSMutableArray *rankURLWithID = [[NSMutableArray alloc] init];
    NSString *rank = @"http://crawler.firstexperience.ru/api/v1/personrank/";
    NSMutableArray *allRates1 = [NSMutableArray array];
    
    for (int i = 0; i < self.sitesID.count; i++) {
        NSString *rankWithId = [NSString stringWithFormat:@"%@%@", rank, self.sitesID[i]];
        NSArray *data = [[NSArray alloc] init];
        NSData *JSONData = [NSData dataWithContentsOfURL:[NSURL URLWithString:rankWithId]];
        NSArray *jsonResult = [NSJSONSerialization JSONObjectWithData:JSONData options:kNilOptions error:nil];
        NSMutableArray *ratesJSON = [NSMutableArray array];
        data = jsonResult;
        for (id item in jsonResult)
            [ratesJSON addObject:[NSString stringWithFormat:@"%@", item[@"rank"]]];
        [allRates1 addObject: ratesJSON];
//        NSLog(@"allRates1 = %@",allRates1);
    }
    
    self.allRates = allRates1;
//    NSLog(@"self.allRates = %@",self.allRates);


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
