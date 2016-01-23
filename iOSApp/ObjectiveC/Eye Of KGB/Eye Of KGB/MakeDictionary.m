//
//  MakeDictionary.m
//  Eye Of KGB
//
//  Created by bu on 21/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "MakeDictionary.h"

@implementation MakeDictionary

//get list of sites from db and put it in array
- (void) getSites {
    self.sites = [[NSMutableArray alloc] initWithObjects: @"Lenta.ru", @"Gazeta.ru", @"RBC.ru", @"Meduza", @"Fontanka.ru", nil];
}

//get rates from db and put it in array
- (void) getRates {
    self.rates = [[NSMutableArray alloc] initWithObjects: @"10500", @"5000", @"1000", nil];
}

//make dictionary with names and rates (from getRates)
- (void) makeDictionary {
    self.names = [[NSMutableArray alloc] initWithObjects: @"Путин", @"Медведев", @"Навальный", nil];
}

@end
