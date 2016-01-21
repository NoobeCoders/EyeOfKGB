//
//  GeneralStatisticViewController.m
//  Eye Of KGB
//
//  Created by bu on 21/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "GeneralStatisticViewController.h"
#import "GeneralStatisticTableViewCell.h"

@interface GeneralStatisticViewController () <UITableViewDataSource, UITableViewDelegate, UIPickerViewDataSource, UIPickerViewDelegate>
@end

@implementation GeneralStatisticViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    self.sitePicker.delegate = self;
    self.sitePicker.dataSource = self;
    
    self.sites = [[NSMutableArray alloc] initWithObjects: @"Lenta.ru", @"Gazeta.ru", @"RBC.ru", nil];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

//MARK: - Delegates and data sources (table)

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return 3;
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    GeneralStatisticTableViewCell *cell = (GeneralStatisticTableViewCell *) [tableView dequeueReusableCellWithIdentifier:@"Cell" forIndexPath:indexPath];
    
    NSArray *names = [[NSArray alloc] initWithObjects: @"Путин", @"Медведев", @"Навальный", nil];
    
    NSArray *counts = [[NSArray alloc] initWithObjects: @"10500", @"5000", @"1000", nil];
    
    cell.labelName.text = names[indexPath.row];
    cell.labelCount.text = counts[indexPath.row];
    
    return cell;
    
}

//MARK: - Delegates and datasource (pickerview)

// returns the number of 'columns' to display.
- (NSInteger)numberOfComponentsInPickerView:(UIPickerView *)pickerView {
    return 1;
}

// returns the # of rows in each component..
- (NSInteger)pickerView:(UIPickerView *)pickerView numberOfRowsInComponent:(NSInteger)component {
    return self.sites.count;
}

- (NSString *)pickerView:(UIPickerView *)thePickerView titleForRow:(NSInteger)row forComponent:(NSInteger)component {
    return [self.sites objectAtIndex:row];
}


@end
