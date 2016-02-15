//
//  DailyStatisticViewController.m
//  Eye Of KGB
//
//  Created by bu on 23/01/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import "DailyStatisticViewController.h"
#import "DailyStatisticTableViewCell.h"
#import "GetData.h"

@interface DailyStatisticViewController ()
@property (weak, nonatomic) IBOutlet UIButton *siteButton;
@property (weak, nonatomic) IBOutlet UIButton *personButton;
@property (weak, nonatomic) IBOutlet UIButton *startDateButton;
@property (weak, nonatomic) IBOutlet UIButton *endDateButton;

@property (assign, nonatomic) NSInteger selectedRowFromSitePicker;
@property (assign, nonatomic) BOOL isItSelectedRowSitePicker;

@property (assign, nonatomic) NSInteger selectedRowFromPersonPicker;
@property (assign, nonatomic) BOOL isItSelectedRowPersonPicker;

@property (strong, nonatomic) NSString *startDateFromDatePicker;
@property (assign, nonatomic) BOOL isItSelectedStartDate;

@property (strong, nonatomic) NSString *endDateFromDatePicker;
@property (assign, nonatomic) BOOL isItSelectedEndDate;

@property (strong, nonatomic) GetData *data;

@property (strong, nonatomic) NSMutableArray *rates;
@property (strong, nonatomic) NSMutableArray *days;

@property (weak, nonatomic) IBOutlet UITableView *tableView;

@end

@implementation DailyStatisticViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    self.isItSelectedRowSitePicker = false;
    self.isItSelectedRowPersonPicker = false;
    self.isItSelectedStartDate = false;
    self.isItSelectedEndDate = false;
    
    self.data = [[GetData alloc] init];
    [self.data getSites];
    [self.data getNames];
    
    // Do any additional setup after loading the view.
}

- (void) viewWillAppear:(BOOL)animated {
    [self configureSiteButton];
    [self configurePersonButton];
    [self configureStartDateButton];
    [self configureEndDateButton];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}


//MARK: - Delegates and data sources (table)

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return self.days.count;
}


- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    DailyStatisticTableViewCell *cell = (DailyStatisticTableViewCell *) [tableView dequeueReusableCellWithIdentifier:@"Cell" forIndexPath:indexPath];
    cell.labelName.text = self.days[indexPath.row];
    cell.labelCount.text = self.rates[indexPath.row];
    
//    cell.labelName.text = @"";
//    cell.labelCount.text = @"";
    
    return cell;
    
}

- (void) configureSiteButton {
    
    if (self.isItSelectedRowSitePicker) {
        NSMutableArray *nameSites = self.data.sites;
        [self.siteButton setTitle:[NSString stringWithFormat:@"%@", nameSites[self.selectedRowFromSitePicker]] forState:(UIControlStateNormal)];
    } else {
        [self.siteButton setTitle:@"<нажмите для выбора>" forState:(UIControlStateNormal)];
    }
}

- (void) configurePersonButton {
    
    if (self.isItSelectedRowPersonPicker) {
        NSMutableArray *nameNames = self.data.names;
        [self.personButton setTitle:[NSString stringWithFormat:@"%@", nameNames[self.selectedRowFromPersonPicker]] forState:(UIControlStateNormal)];
    } else {
        [self.personButton setTitle:@"<нажмите для выбора>" forState:(UIControlStateNormal)];
    }
}

- (void) configureStartDateButton {
    if (self.isItSelectedStartDate) {
        [self.startDateButton setTitle:[NSString stringWithFormat:@"%@", self.startDateFromDatePicker] forState:(UIControlStateNormal)];
    } else {
        [self.startDateButton setTitle:@"<нажмите для выбора>" forState:(UIControlStateNormal)];
    }
}

- (void) configureEndDateButton {
    if (self.isItSelectedEndDate) {
        [self.endDateButton setTitle:[NSString stringWithFormat:@"%@", self.endDateFromDatePicker] forState:(UIControlStateNormal)];
    } else {
        [self.endDateButton setTitle:@"<нажмите для выбора>" forState:(UIControlStateNormal)];
    }
}

- (IBAction)siteButton:(id)sender {
    SitePickerDailyStatisticViewController *sitePickerDailyStatisticViewController = [self.storyboard instantiateViewControllerWithIdentifier:@"SitePickerDailyStatistic"];
    sitePickerDailyStatisticViewController.delegate = self;
    [self presentViewController:sitePickerDailyStatisticViewController animated:YES completion:NULL];
}


- (IBAction)personButton:(id)sender {
    PersonPickerDailyStatisticViewController *personPickerDailyStatisticViewController = [self.storyboard instantiateViewControllerWithIdentifier:@"PersonPickerViewController"];
    personPickerDailyStatisticViewController.delegate = self;
    [self presentViewController:personPickerDailyStatisticViewController animated:YES completion:NULL];
}

- (IBAction)startDateButton:(id)sender {
    StartDatePickerViewController *startDatePickerViewController = [self.storyboard instantiateViewControllerWithIdentifier:@"StartDatePickerViewController"];
    startDatePickerViewController.delegate = self;
    [self presentViewController:startDatePickerViewController animated:YES completion:NULL];
}
- (IBAction)endDateButton:(id)sender {
    EndDatePickerViewController *endDatePickerViewController = [self.storyboard instantiateViewControllerWithIdentifier:@"EndDatePickerViewController"];
    endDatePickerViewController.delegate = self;
    [self presentViewController:endDatePickerViewController animated:YES completion:NULL];
}


- (void) setSelectedRowSitePicker:(NSInteger)selectedRow andBoolForSiteButtonName:(BOOL)boolForSiteButtonName {
    self.selectedRowFromSitePicker = selectedRow;
    self.isItSelectedRowSitePicker = boolForSiteButtonName;
}


- (void) setSelectedRowPersonPicker:(NSInteger)selectedRow andBoolForPersonButtonName:(BOOL)boolForPersonButtonName {
    self.selectedRowFromPersonPicker = selectedRow;
    self.isItSelectedRowPersonPicker = boolForPersonButtonName;
}

- (void) setStartDate:(NSString *)startDate andBoolForStartDateButtonName:(BOOL)boolForStartDateButtonName {
    self.startDateFromDatePicker = startDate;
    self.isItSelectedStartDate = boolForStartDateButtonName;
}

- (void) setEndDate:(NSString *)endDate andBoolForEndDateButtonName:(BOOL)boolForEndDateButtonName {
    self.endDateFromDatePicker = endDate;
    self.isItSelectedEndDate = boolForEndDateButtonName;
}

- (IBAction)applyButton:(id)sender {
    [self getEveryDayRates];
}

//Dont know how do it in GetData?
- (void) getEveryDayRates {
    NSString *generalRankURL = @"http://crawler.firstexperience.ru/api/v1/rankeveryday/";
    NSString *rankWithIdURL = [NSString stringWithFormat:@"%@%@/?dt_range=%@,%@", generalRankURL, self.data.sitesID[self.selectedRowFromSitePicker], self.startDateFromDatePicker, self.endDateFromDatePicker];
    NSLog(@"url = %@",rankWithIdURL);

    NSArray *data = [[NSArray alloc] init];
    NSData *JSONData = [NSData dataWithContentsOfURL:[NSURL URLWithString:rankWithIdURL]];
    NSArray *jsonResult = [NSJSONSerialization JSONObjectWithData:JSONData options:kNilOptions error:nil];
    NSMutableArray *ratesJSON = [NSMutableArray array];
    NSMutableArray *daysJSON = [NSMutableArray array];
    data = jsonResult;
    for (id item in jsonResult) {
        [ratesJSON addObject:[NSString stringWithFormat:@"%@", item[@"rank_day"]]];
        [daysJSON addObject:[NSString stringWithFormat:@"%@", item[@"data_scan"]]];

    }
    
    NSInteger numberOfDays = daysJSON.count;
    self.rates = [[NSMutableArray alloc] init];
    self.days = [[NSMutableArray alloc] init];

    switch (self.selectedRowFromPersonPicker) {
        case 0:
            
            for (int i = 0; i < numberOfDays; i = i+3) {
                [self.rates addObject:ratesJSON[i]];
                [self.days addObject:daysJSON[i]];
            }
            [self.tableView reloadData];
            
            break;
        case 1:
            for (int i = 1; i < numberOfDays; i = i+3) {
                [self.rates addObject:ratesJSON[i]];
                [self.days addObject:daysJSON[i]];
            }
            [self.tableView reloadData];
            break;
        case 2:
            for (int i = 2; i < numberOfDays; i = i+3) {
                [self.rates addObject:ratesJSON[i]];
                [self.days addObject:daysJSON[i]];
            }
            [self.tableView reloadData];
            break;
        default:
            break;
    }
}

@end
