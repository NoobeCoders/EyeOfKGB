//
//  SitePickerDailyStatisticViewController.h
//  Eye Of KGB
//
//  Created by bu on 05/02/16.
//  Copyright © 2016 Oleg Shamin. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol SitePickerViewControllerDelegate <NSObject>
- (void)setSelectedRowSitePicker:(NSInteger)selectedRow andBoolForSiteButtonName:(BOOL)boolForSiteButtonName;
@end

@interface SitePickerDailyStatisticViewController : UIViewController

@property (nonatomic, strong) id <SitePickerViewControllerDelegate> delegate;

@end
