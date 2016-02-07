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
@end

@implementation DailyStatisticViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    self.isItSelected = false;
    
    // Do any additional setup after loading the view.
}

- (void) viewWillAppear:(BOOL)animated {
    
    [self configureSiteButton];
    
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
    
    DailyStatisticTableViewCell *cell = (DailyStatisticTableViewCell *) [tableView dequeueReusableCellWithIdentifier:@"Cell" forIndexPath:indexPath];
    
    cell.labelName.text = @"";
    cell.labelCount.text = @"";
    
    return cell;
    
}

- (void) configureSiteButton {
    
    if (self.isItSelected) {
        GetData *data = [[GetData alloc] init];
        [data getSites];
        NSMutableArray *nameSites = data.sites;
        [self.siteButton setTitle:[NSString stringWithFormat:@"%@", nameSites[self.selectedRow]] forState:(UIControlStateNormal)];
    } else {
        [self.siteButton setTitle:@"<нажмите для выбора>" forState:(UIControlStateNormal)];
    }
}

@end
